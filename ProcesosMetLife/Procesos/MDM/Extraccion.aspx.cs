using System;

namespace ProcesosMetLife.Procesos.MDM
{
    public partial class Extraccion : Utilerias.Comun
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            manejo_sesion = (IU.ManejadorSesion)Session["Sesion"];
            mensajes.TituloAplicacionEnUso(this, "WFO Recuparar Cartas IMSS (KWIK)");

            if (manejo_sesion == null)
                Response.Redirect("\\Default.aspx");

            if (!IsPostBack)
            {

            }
        }

        protected void BtnExcelAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                lblProcesadoExcel.ForeColor = System.Drawing.Color.OrangeRed;
                lblProcesadoExcel.Text = "Inicia el Proceso. Favor de no cerrar la ventana";
                lblProcesadoExcel.Visible = true;

                string rutaArchivo = Server.MapPath("~/ArchivosTemporales/");
                string nombreArchivo = AsyncFileUpload1.FileName;
                string extension = nombreArchivo.Substring(nombreArchivo.LastIndexOf(".")+ 1);
                AsyncFileUpload1.SaveAs(rutaArchivo + nombreArchivo);

                // TODO: Para obtimizar y cerrar el proceso, al enviar bulk hay que agregarle el id del usuario para que después al proceso de creación de trámites se haga sobre los que subió el usuario (pensando que varios usuarios pueden subir el insumo (extracción al mismo tiempo) )
                // TODO: Mejora. Se debe indicar la cantidad de trámites creados.

                //DateTime fI = DateTime.Now;
                i.mdm.extraccion.ProcesarExcel(rutaArchivo, nombreArchivo, extension);
                //DateTime fFB = DateTime.Now;
                i.mdm.extraccion.CreateTramite_onExtraccion();
                //DateTime fFA = DateTime.Now;

                lblProcesadoExcel.ForeColor = System.Drawing.Color.DarkGreen;
                lblProcesadoExcel.Text = "Proceso Terminado...     "; // + fI.ToString("HH.mm.ss.fff") + "||" + fFB.ToString("HH.mm.ss.fff") + "||" + fFA.ToString("HH.mm.ss.fff") ;
                lblProcesadoExcel.Visible = true;
            }
            catch (Exception ex)
            {
                lblProcesadoExcel.Text = "No se pudo guardar el archivo: asegúrese de cargarlo apropiadamente.";
                lblProcesadoExcel.ForeColor = System.Drawing.Color.Red;
                lblProcesadoExcel.Visible = true;

                // TODO: Pendiente el manejo de LOG.
                //log.AgregarError(ex.Message.ToString());
            }
        }
    }
}