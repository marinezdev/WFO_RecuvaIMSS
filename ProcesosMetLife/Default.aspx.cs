using System;
using System.Web;

namespace ProcesosMetLife
{
    public partial class Default : Utilerias.Comun
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!SM1.IsInAsyncPostBack)
                Session["timeout"] = DateTime.Now.AddMinutes(double.Parse(manejo_sesion.EsperaBloqueo)).ToString();
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            try
            {
                string mensaje = string.Empty;
                string ruta = string.Empty;
                string menuApp = string.Empty;

                if (i.administracion.login.Autenticar(txUsuario.Text, txClave.Text, ref mensaje, ref manejo_sesion, ref menuApp) == -1)
                {
                    // TODO: Revisión del manejo de LOG
                    // log.Agregar(txUsuario.Text + " ha intentado ingresar al sistema, ha equivocado su clave o intenta accesar sin autorización.");
                    mensajes.MostrarMensaje(this, mensaje);
                }
                else
                {
                    manejo_sesion.Cla = txUsuario.Text;
                    manejo_sesion.Con = txClave.Text;
                    manejo_sesion.MensajeAdvertencia = "";
                    manejo_sesion.Menu = menuApp;
                    Session["IdSesion"] = HttpContext.Current.Session.SessionID;
                    Session["Sesion"] = manejo_sesion;
                    Response.Redirect(manejo_sesion.Usuarios.RolAcceso, false);
                }
            }
            catch (Exception ex)
            {
                // TODO: Revisión del manejo de LOG
                // log.Agregar(ex);
            }
        }

        private void UpdateTimer()
        {
            Label1.Text = DateTime.Now.ToLongTimeString();
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            //if (0 > DateTime.Compare(DateTime.Now, Funciones.Fechas.ConvertirTextoAFecha(Session["timeout"].ToString())))
            //    Label1.Text = "Quedan " + ((Int32)Funciones.Fechas.ConvertirTextoAFecha(Session["timeout"].ToString()).Subtract(DateTime.Now).TotalMinutes).ToString() + " minutos para desbloquear. <br /> No cierre el navegador.";
            //else
            //{
            //    try
            //    {
            //        i.administracion.usuarios.ActualizarDesconectarSesion(Funciones.Nums.TextoAEntero(Session["idusuario"].ToString()), 0, manejo_sesion.IdParaCierreSesion);
            //        Session["idusuario"] = null;
            //        LblMensajes.Text = "";
            //        Label1.Text = "";
            //        Response.Redirect("Default.aspx", true);
            //    }
            //    catch
            //    {
            //    }
            //}
        }
    }
}