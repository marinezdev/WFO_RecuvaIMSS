﻿using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcesosMetLife.AccesoDatos.ISSSTE.Procesos
{
    public class Tramite_Asigna_Futuro
    {
        ManejoDatos b = new ManejoDatos();

        public int AgregarUsuarioFuturo(string idusuario, string idusuarioasigna, string idtramite)
        {
            b.ExecuteCommandSP("Tramite_Asigna_Futuro_Agregar");
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
            b.AddParameter("@idusuarioasigna", idusuarioasigna, SqlDbType.Int);
            b.AddParameter("@idtramite", idtramite, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }
    }
}
