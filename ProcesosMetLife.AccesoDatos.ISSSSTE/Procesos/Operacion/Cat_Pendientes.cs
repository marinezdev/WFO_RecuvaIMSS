﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prop = ProcesosMetLife.Propiedades.Procesos.Operacion;

namespace ProcesosMetLife.AccesoDatos.ISSSTE.Procesos.Operacion
{
    public class Cat_Pendientes
    {
        ManejoDatos b = new ManejoDatos();

        public List<prop.Cat_Pendientes> SelecionarPendientes(int Id_Usuario)
        {
            b.ExecuteCommandSP("Cat_Pendientes_Selecionar_Usuario");
            b.AddParameter("@Id_Usuario", Id_Usuario, SqlDbType.Int);
            List<prop.Cat_Pendientes> resultado = new List<prop.Cat_Pendientes>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.Cat_Pendientes item = new prop.Cat_Pendientes()
                {
                    Id_Pendiente    = Funciones.Nums.TextoAEntero(reader["Id_Pendiente"].ToString()),
                    Nombre          = reader["Nombre"].ToString(),
                    Icono           = reader["Icono"].ToString(),
                    Total           = Funciones.Nums.TextoAEntero(reader["Total"].ToString()),
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

    }
}
