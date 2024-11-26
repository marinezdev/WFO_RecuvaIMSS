using System;
using System.Collections.Generic;
using System.Data;
using prop = ProcesosMetLife.Propiedades;

namespace ProcesosMetLife.Negocio.Sistema
{
    public class Login
    {
        AccesoDatos.Sistema.Usuarios usuarios = new AccesoDatos.Sistema.Usuarios();

        public int Autenticar(string clave, string contraseña, ref string mensaje, ref IU.ManejadorSesion sesion, ref string menuApp)
        {
            int resultado = -1;
            DataSet dsResultadoAccceso = usuarios.ValidarAcceso(clave, contraseña, ref sesion);
            mensaje = dsResultadoAccceso.Tables[0].Rows[0][1].ToString();
            resultado = int.Parse(dsResultadoAccceso.Tables[0].Rows[0][0].ToString());

            if (resultado != -1)
            {
                menuApp = CrearMenuHTML(dsResultadoAccceso.Tables[1]);
            }
            return resultado;
        }

        private string CrearMenuHTML(DataTable dtMenu)
        {
            string menuConstruido = "";
            try 
            {
                menuConstruido = "<div id='sidebar-menu' class='main_menu_side hidden-print main_menu'><div class='menu_section'>" +
                         "<ul class='nav side-menu'>";

                foreach (DataRow dr in dtMenu.Rows)
                {
                    // TODO: Realizar la creación del Menú Jerarquíco. Hacer la función recursiva...
                    menuConstruido += "<li><a href='" + dr["URL"].ToString() + "'><i class='fa " + dr["Icono"].ToString() + "'></i> " + dr["Descripcion"].ToString() + "<span class='fa fa-chevron-down'></span></a></li>";
                }

                menuConstruido += "</ul>" +
                            "</div>" +
                        "</div>";

            }
            catch (Exception)
            {
                menuConstruido = "";
            }

            //foreach (var padre in mp)
            //{
            //    if (int.Parse(padre.PerteneceA.ToString()) == 0)
            //    {
            //        List<prop.Menu> Nivel1 = new List<prop.Menu>();
            //        // if (padre.IdMenu == menu.SeleccionarHijos(idrol.ToString()))
            //        if (padre.IdMenu == menu.SeleccionarHijos(padre.IdMenu.ToString(), app))
            //            menuConstruido += "<li><a href='" + padre.URL + "'><i class='fa " + padre.Icono + "'></i> " + padre.Descripcion + "<span class='fa fa-chevron-down'></span></a>";
            //        else
            //            menuConstruido += "<li><a href='" + padre.URL + "'><i class='fa " + padre.Icono + "'></i> " + padre.Descripcion + "</a>";
            //        foreach (var hijo in mp)
            //        {
            //            if (int.Parse(hijo.PerteneceA.ToString()) == padre.IdMenu)
            //            {
            //                Nivel1.Add(hijo);
            //            }
            //        }
            //        if (Nivel1.Count > 0)
            //        {
            //            menuConstruido += "<ul class='nav child_menu'>";
            //            foreach (var Hijos in Nivel1)
            //            {
            //                menuConstruido += "<li><a href='" + Hijos.URL + "'>" + Hijos.Descripcion + "</a>";
            //                List<prop.Menu> Nivel2 = new List<prop.Menu>();
            //                foreach (var padre2 in mp)
            //                {
            //                    if (int.Parse(padre2.PerteneceA.ToString()) == Hijos.IdMenu)
            //                    {
            //                        Nivel2.Add(padre2);
            //                    }
            //                }
            //                if (Nivel2.Count > 0)
            //                {
            //                    menuConstruido += "<ul class='nav child_menu'>";
            //                    foreach (var Hijos2 in Nivel2)
            //                    {
            //                        menuConstruido += "<li><a href='" + Hijos2.URL + "'>" + Hijos2.Descripcion + "<span class='fa fa-chevron-down'></span></a>";
            //                    }
            //                    menuConstruido += "</ul>";
            //                }
            //            }
            //            menuConstruido += "</ul>";
            //        }
            //        menuConstruido += "</li>";
            //    }
            //}

            
            return menuConstruido;
        }
    }
}
