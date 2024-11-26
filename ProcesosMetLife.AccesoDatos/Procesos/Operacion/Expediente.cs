﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prop = ProcesosMetLife.Propiedades.Procesos.Operacion;

namespace ProcesosMetLife.AccesoDatos.Procesos.Operacion
{
    public class Expediente
    {
        ManejoDatos b = new ManejoDatos();

        public List<prop.Expediente> Expediente_Consultar_PorIdTramite(int IdTramite)
        {
            b.ExecuteCommandSP("Expediente_Consultar_PorIdTramite");
            b.AddParameter("@IdTramite", IdTramite, SqlDbType.Int);
            List<prop.Expediente> resultado = new List<prop.Expediente>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.Expediente item = new prop.Expediente()
                {
                    IdExpediente = Funciones.Nums.TextoAEntero(reader["IdExpediente"].ToString()),
                    IdTramite = Funciones.Nums.TextoAEntero(reader["IdTramite"].ToString()),
                    NmArchivo = reader["NmArchivo"].ToString(),
                    Fecha_Registro = Convert.ToDateTime(reader["Fecha_Registro"].ToString()),
                    Fusion = reader["Fusion"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }
    }
}
