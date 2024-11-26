﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProcesosMetLife.Procesos.MDM.Supervisor
{
    public partial class MapaGeneral : Utilerias.Comun
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            manejo_sesion = (IU.ManejadorSesion)Session["Sesion"];
            if (manejo_sesion == null)
                Response.Redirect("\\Default.aspx");

            if (!IsPostBack)
            {
                if (!String.IsNullOrEmpty(Request.QueryString["msj"]))
                {
                    if (Request.QueryString["msj"].ToString() == "1")
                        mensajes.MostrarMensaje(this, "No hay trámites disponibles...");
                }

                CargaFlujos(manejo_sesion.Usuarios.IdUsuario);
            }
        }

        protected void CargaFlujos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int IdFlujo = Convert.ToInt32(cbFlujos.SelectedValue.ToString());
            PintaMesas(IdFlujo);
        }

        protected void CargaFlujos(int Id)
        {
            i.mdm.captura2.SeleccionarFlujo_DropDownList(ref cbFlujos, Id);
        }

        protected void PintaMesas(int IdFlujo)
        {
            i.mdm.captura2.Dashboard(ref MesasLiteral, IdFlujo);
        }
    }
}