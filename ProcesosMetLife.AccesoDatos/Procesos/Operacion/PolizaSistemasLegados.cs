﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcesosMetLife.AccesoDatos.Procesos.Operacion
{
    public class PolizaSistemasLegados
    {
        ManejoDatos b = new ManejoDatos();

        public int ActualizaPolizaSistemasLegados(int IdTramite, int IdUsuario, string IdSisLegados)
        {
            b.ExecuteCommandSP("Tramite_Actualiza_PolizaSisLegados");
            b.AddParameter("@IdTramite", IdTramite, SqlDbType.Int, 150);
            b.AddParameter("@IdUsuario", IdUsuario, SqlDbType.Int, 150);
            b.AddParameter("@IdSisLegados", IdSisLegados, SqlDbType.VarChar, 150);
            return b.InsertUpdateDeleteWithTransaction();
        }

        public int AgregarInteractivo(int IdTramite)
        {
            b.ExecuteCommandSP("Tramite_Actualiza_ProcesoInteractivo");
            b.AddParameter("@IdTramite", IdTramite, SqlDbType.Int, 150);
            return b.InsertUpdateDeleteWithTransaction();
        }
    }
}
