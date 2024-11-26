using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;
using prop = ProcesosMetLife.Propiedades.Procesos.Operacion;

namespace ProcesosMetLife.Procesos.MDM.Operador
{
    public partial class TramiteProcesar2 : Utilerias.Comun
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            manejo_sesion = (IU.ManejadorSesion)Session["Sesion"];
            if (manejo_sesion == null)
                Response.Redirect("\\Default.aspx");

            if (!IsPostBack)
            {
                hfIdFlujo.Value = int.Parse(Request["IdFlujo"]).ToString();
                hfIdMesa.Value = int.Parse(Request["IdMesa"]).ToString();

                if (Request["IdTramite"] == null || string.IsNullOrWhiteSpace(Request["IdTramite"]))
                {
                    hfIdTramite.Value = "0";
                }
                else
                {
                    hfIdTramite.Value = int.Parse(Request.Form["IdTramite"]).ToString();
                }
                PintaMesa();
            }
        }

        protected void PintaMesa()
        {
            ControlsClear();
            MotivosRechazos();

            DataRow _TramiteProcesar = null;
            _TramiteProcesar = i.mdm.tramitemesa.AsignarTramite(int.Parse(hfIdMesa.Value.ToString()), manejo_sesion.Usuarios.IdUsuario, int.Parse(hfIdTramite.Value.ToString()) );

            hfIdTramite.Value = _TramiteProcesar["Id"].ToString(); 

            if (_TramiteProcesar[2].ToString() == "KO")
            {
                mensajes.MostrarMensaje(this, _TramiteProcesar[3].ToString(), "MapaGeneral.aspx");
            }
            else 
            {
                if (_TramiteProcesar[0].ToString() == "0")
                {
                    mensajes.MostrarMensaje(this, "No existen trámites a procesar en la mesa...", "MapaGeneral.aspx");
                }
                else
                {
                    lblArchivoInfo.Text = "Archivo no cargado....";
                    lblArchivoInfo.Visible = true;
                    txtMatricula.Text = _TramiteProcesar["Matricula"].ToString();
                    txtPoliza.Text = _TramiteProcesar["Poliza"].ToString();
                    txtQuincena.Text = _TramiteProcesar["Quincena"].ToString();
                    txtUP.Text = _TramiteProcesar["UnidadPago"].ToString();
                    txtTNomina.Text = _TramiteProcesar["TipoNomina"].ToString();
                    txtTMovimiento.Text = _TramiteProcesar["TipoMovimiento"].ToString();
                    txtImporte.Text = _TramiteProcesar["Importe"].ToString();
                    txtPolizaPortal.Text = _TramiteProcesar["PolizaPortal"].ToString();
                }
            }
        }

        protected void MotivosRechazos()
        {
            DataTable Rechazos = i.mdm.tramitemesa.RechazosGet( int.Parse(hfIdMesa.Value.ToString()) );
            foreach (DataRow row in Rechazos.Rows)
            {
                TreeNode node = new TreeNode("     " + row["Nombre"].ToString(), row["Id"].ToString());
                node.SelectAction = TreeNodeSelectAction.None;

                //DataSet childNodes = GetRowsWhereParentIDEquals(row["topicId"]);
                //foreach (DataRow child in childNodes.Rows)
                //{
                //    Treenode childNode = new TreeNode(..Whatever);
                //    node.Nodes.add(childNode);
                //}
                tvRechazos.Nodes.Add(node);
            }
        }

        protected void ControlsClear()
        {
            txtObservacionesRechazo.Text = string.Empty;
            txtObservacionesPausar.Text = string.Empty;
            txtMatricula.Text = string.Empty;
            txtPoliza.Text = string.Empty;
            txtQuincena.Text = string.Empty;
            txtUP.Text = string.Empty;
            txtTNomina.Text = string.Empty;
            txtTMovimiento.Text = string.Empty;
            txtImporte.Text = string.Empty;
            txtPolizaPortal.Text = string.Empty;
            //hfIdFlujo.Value = string.Empty;
            //hfIdMesa.Value = string.Empty;
            //hfIdTramite.Value = string.Empty;
            hfArchivo.Value = string.Empty;

            UploadFiles.Visible = true;
            btnAceptar.Enabled = false;
            btnCargarArchivo.Enabled = true;
        }

        protected void TramiteProcesar(int IdStatusMesa, string ObsPub, string ObsPrv, List<int> MotivosRechazos)
        {
            string _respuesta = i.mdm.tramitemesa.ProcesarTramite(int.Parse(hfIdTramite.Value.ToString()), int.Parse(hfIdMesa.Value.ToString()), manejo_sesion.Usuarios.IdUsuario, IdStatusMesa, ObsPub, ObsPrv, MotivosRechazos);
            // "2:Trámite Procesado"
            string[] obtenidos = _respuesta.Split(':');
            if (obtenidos[0] != "0" && obtenidos[1] == "Trámite Procesado")
            {
                Response.Redirect("TramiteProcesar2.aspx?IdFlujo=" + hfIdFlujo.Value.ToString() + "&IdMesa=" + hfIdMesa.Value.ToString(), true);
            }
            else 
            {
                mensajes.MostrarMensaje(this, "Algo salío mal!\\n * " + obtenidos[1]);
            }
        }

        protected void btnRechazar_Click(object sender, EventArgs e)
        {
            bool validation = true;
            string validacionmsj = "Validación.";
            List<int> motivos = new List<int>();

            foreach (TreeNode nodo in tvRechazos.Nodes)
            {
                if (nodo.Checked)
                {
                    motivos.Add(int.Parse(nodo.Value.ToString()));
                }
            }

            if (motivos.Count == 0)
            {
                validation = false;
                validacionmsj += "\\n\\n * Debe indicar los motivos del rechazo.";
            }

            if (txtObservacionesRechazo.Text.Trim().Length < 5)
            {
                validation = false;
                validacionmsj += "\\n\\n * Debe indicar las observaciones para pausar el trámite.";
            }
            
            if (validation)
            {
                // TODO: Los Ids de los status deberan procesarse desde un enumeador
                TramiteProcesar(5, "Pausa Trámite", txtObservacionesPausar.Text.Trim(), motivos);
            }
            else
            {
                // TODO: Mejorar los mensajes de advertencia a como se maneja en IMSS Portal.
                mensajes.MostrarMensaje(this, validacionmsj);
            }

        }

        protected void btnPausar_Click(object sender, EventArgs e)
        {
            bool validation = true;
            string validacionmsj = "Validación.";

            if (txtObservacionesPausar.Text.Trim().Length < 5)
            {
                validation = false;
                validacionmsj += "\\n\\n * Debe indicar las observaciones para pausar el trámite.";
            }

            if (validation)
            {
                // TODO: Los Ids de los status deberan procesarse desde un enumeador
                TramiteProcesar(3, "Pausa Trámite", txtObservacionesPausar.Text.Trim(), null);
            }
            else
            {
                // TODO: Mejorar los mensajes de advertencia a como se maneja en IMSS Portal.
                mensajes.MostrarMensaje(this, validacionmsj);
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            bool validation = true;
            string validationmsj = "Validación\\n";
            string archivo = "";
            string ruta= "";
            string rutafinal = "";
            string archivoFinal = "";

            string promotoria = "XX";

            ruta = Server.MapPath("~/Cartas/");
            archivo = hfArchivo.Value.ToString();


            if (archivo.Length == 0)
            {
                validation = false;
                validationmsj = "\\n * No se ha cargado la carta para asociarla al trámite.";
            }
            
            if (!File.Exists(archivo))
            {
                validation = false;
                validationmsj = "\\n * No se encontró la carta que se cargó.";
            }

            if (validation)
            {
                rutafinal = ruta + txtQuincena.Text.Trim() + "_" + txtTNomina.Text.Trim();
                archivoFinal = promotoria + "_" + txtPoliza.Text + "_" + txtUP.Text + "_" + txtQuincena.Text + "_" + txtImporte.Text.Replace(".","-") + ".pdf";

                if (!Directory.Exists(rutafinal))
                {
                    Directory.CreateDirectory(rutafinal);
                }

                File.Copy(archivo, rutafinal + "\\" + archivoFinal);
                if (File.Exists(rutafinal + "\\" + archivoFinal))
                {
                    TramiteProcesar(4,"","Trámite Procesado",null);
                }
                else
                {
                    mensajes.MostrarMensaje(this, "No se realizó la recuperacion de la carta");
                }
            }
            else
            {
                mensajes.MostrarMensaje(this, validationmsj);
            }
        }

        protected void btnCargarArchivo_Click(object sender, EventArgs e)
        {
            try
            {
                lblArchivoInfo.ForeColor = System.Drawing.Color.OrangeRed;
                lblArchivoInfo.Text = "Inicia el Proceso. Favor de no cerrar la ventana";
                lblArchivoInfo.Visible = true;
                //mensajes.MostrarMensaje(this, lblArchivoInfo.Text);

                string rutaArchivo = Server.MapPath("~/CartasTemporales/");
                string nombreArchivo = AsyncFileUpload1.FileName;
                string extension = nombreArchivo.Substring(nombreArchivo.LastIndexOf(".") + 1);
                string nombreArchivoUP = DateTime.Now.ToString("yyMMddHHmmssfff") + manejo_sesion.Usuarios.IdUsuario.ToString() + "." + extension;

                if (extension.ToUpper() == "PDF")
                {
                    AsyncFileUpload1.SaveAs(rutaArchivo + nombreArchivoUP);

                    if (File.Exists( rutaArchivo + nombreArchivoUP ))
                    {
                        ltMuestraPdf.Text = "<embed src='\\CartasTemporales\\" + nombreArchivoUP + "' style='width:100%; height:100%' type='application/pdf'>";

                        hfArchivo.Value = rutaArchivo + nombreArchivoUP;
                        UploadFiles.Visible = false;
                        btnAceptar.Enabled = true;
                        btnCargarArchivo.Enabled = false;

                        lblArchivoInfo.ForeColor = System.Drawing.Color.DarkGreen;
                        lblArchivoInfo.Text = "Archivo Cargado...     "; // + fI.ToString("HH.mm.ss.fff") + "||" + fFB.ToString("HH.mm.ss.fff") + "||" + fFA.ToString("HH.mm.ss.fff") ;
                        lblArchivoInfo.Visible = true;
                    }
                    else
                    {
                        lblArchivoInfo.Text = "Archivo no encontrado";
                        lblArchivoInfo.ForeColor = System.Drawing.Color.Red;
                        lblArchivoInfo.Visible = true;
                        mensajes.MostrarMensaje(this, lblArchivoInfo.Text);
                    }
                }
                else
                {
                    lblArchivoInfo.Text = "Tipo de archivo inválido.";
                    lblArchivoInfo.ForeColor = System.Drawing.Color.Red;
                    lblArchivoInfo.Visible = true;
                    mensajes.MostrarMensaje(this, lblArchivoInfo.Text);
                }
            }
            catch (Exception)
            {
                lblArchivoInfo.Text = "No se pudo guardar el archivo: asegúrese de cargarlo apropiadamente.";
                lblArchivoInfo.ForeColor = System.Drawing.Color.Red;
                lblArchivoInfo.Visible = true;
                mensajes.MostrarMensaje(this, lblArchivoInfo.Text);

                // TODO: Pendiente el manejo de LOG.
                //log.AgregarError(ex.Message.ToString());
            }

        }

    }
}