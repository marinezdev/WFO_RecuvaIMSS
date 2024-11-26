﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prop = ProcesosMetLife.Propiedades.Procesos.Promotoria;

namespace ProcesosMetLife.AccesoDatos.Procesos.Promotoria
{
    public class Archivos
    {
        ManejoDatos b = new ManejoDatos();

        public List<prop.control_archivos> ControlArchivoNuevoID()
        {
            b.ExecuteCommandSP("Control_Archivos_Selecionar");
            List<prop.control_archivos> resultado = new List<prop.control_archivos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.control_archivos item = new prop.control_archivos()
                {
                    Id = Funciones.Nums.TextoAEntero(reader["Id"].ToString())
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }
    }
}
