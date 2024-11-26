﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using prop = ProcesosMetLife.Propiedades;

namespace ProcesosMetLife.Negocio.Sistema
{
    public class Menu
    {
        AccesoDatos.Sistema.Menu menu = new AccesoDatos.Sistema.Menu();

        public void Seleccionar_GridView(ref GridView gridview)
        {
            Funciones.LlenarControles.LlenarGridView<prop.Menu>(ref gridview, menu.Seleccionar());
        }

        public void SeleccionarPertenencia(ref DropDownList dropdownlist)
        {
            Funciones.LlenarControles.LlenarDropDownList<prop.Menu>(ref dropdownlist, menu.SeleccionarPertenencia(), "Descripcion", "IdMenu");
        }

        public void LlenarTreeView(ref TreeView treeview, int idrol)
        {
            Funciones.LlenarControles.LlenarTreeViewMenu(menu.SeleccionarAsignadosPorRol(idrol), null, ref treeview);
        }

        /// <summary>
        /// Obtiene un menú creado dinámicamente para guardar en una variable y mostrarlo en todo el sistema
        /// </summary>
        /// <param name="idrol">Rol del que se cerará el menú</param>
        /// <returns>Cadena con el menú creado en HTML</returns>
        public string Seleccionar(int idrol, string app)
        {
            return CrearMenuHTML(idrol, app);
        }

        public prop.Menu SeleccionarPorId(int id)
        {
            return menu.SeleccionarPorId(id);
        }

        public List<prop.PermisosMenuRol> SeleccionarAsignadosPorRol(int idrol)
        {
            return menu.SeleccionarAsignadosPorRol(idrol);
        }

        public void SeleccionarIconos_DropDownList(ref DropDownList dropdownlist)
        {
            Funciones.LlenarControles.LlenarDropDownList(ref dropdownlist, menu.SeleccionarIconos(), "Icono", "Icono");
        }

        //
        /// <summary>
        /// Menú que se construye dinámicamente por base de datos (3 subniveles)
        /// </summary>
        /// <returns>cadena con el menú construído</returns>
        private string CrearMenuHTML(int idrol, string app)
        {
            List<prop.Menu> mp = new List<prop.Menu>();
            mp = menu.MenuDinamicoObtener(idrol, app);

            string menuConstruido = "<div id='sidebar-menu' class='main_menu_side hidden-print main_menu'><div class='menu_section'>" +
                     "<ul class='nav side-menu'>";

            foreach (var padre in mp)
            {
                if (int.Parse(padre.PerteneceA.ToString()) == 0)
                {
                    List<prop.Menu> Nivel1 = new List<prop.Menu>();
                    // if (padre.IdMenu == menu.SeleccionarHijos(idrol.ToString()))
                    if (padre.IdMenu == menu.SeleccionarHijos(padre.IdMenu.ToString(), app))
                        menuConstruido += "<li><a href='" + padre.URL + "'><i class='fa " + padre.Icono + "'></i> " + padre.Descripcion + "<span class='fa fa-chevron-down'></span></a>";
                    else
                        menuConstruido += "<li><a href='" + padre.URL + "'><i class='fa " + padre.Icono + "'></i> " + padre.Descripcion + "</a>";
                    foreach (var hijo in mp)
                    {
                        if (int.Parse(hijo.PerteneceA.ToString()) == padre.IdMenu)
                        {
                            Nivel1.Add(hijo);
                        }
                    }

                    if(Nivel1.Count > 0)
                    {
                        menuConstruido += "<ul class='nav child_menu'>";
                        foreach (var Hijos in Nivel1)
                        {
                            menuConstruido += "<li><a href='" + Hijos.URL + "'>" + Hijos.Descripcion + "</a>";
                            List<prop.Menu> Nivel2 = new List<prop.Menu>();
                            foreach (var padre2 in mp)
                            {
                                if (int.Parse(padre2.PerteneceA.ToString()) == Hijos.IdMenu)
                                {
                                    Nivel2.Add(padre2);
                                }
                            }

                            if (Nivel2.Count > 0)
                            {
                                menuConstruido += "<ul class='nav child_menu'>";
                                foreach (var Hijos2 in Nivel2)
                                {
                                    menuConstruido += "<li><a href='" + Hijos2.URL + "'>" + Hijos2.Descripcion + "<span class='fa fa-chevron-down'></span></a>";
                                }
                                menuConstruido += "</ul>";
                            }
                        }
                        menuConstruido += "</ul>";
                    }
                    menuConstruido += "</li>";
                }
            }

            menuConstruido += "</ul>" +
                "</div>" +
            "</div>"; ;

            return menuConstruido;
        }

        public int Agregar(string descripcion, string url, string icono, int pertenecea, string categoria, int orden)
        {
            return menu.Agregar(descripcion, url, icono, pertenecea, categoria, orden);
        }

        public int Modificar(string descripcion, string url, string icono,  int pertenecea, string categoria, int orden, int idmenu)
        {
            return menu.Modificar(descripcion, url, icono, pertenecea, categoria, orden, idmenu);
        }


    }
}
