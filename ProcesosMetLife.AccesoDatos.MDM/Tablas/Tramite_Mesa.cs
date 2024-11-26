using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcesosMetLife.AccesoDatos.MDM.Tablas
{
    public class Tramite_Mesa
    {
        ManejoDatos b = new ManejoDatos();

        public DataRow AsignarTramite(int IdMesa, int IdUsuario, int IdTramite)
        {
            b.ExecuteCommandSP("WFO_RecuvaIMSS.dbo.spWFOTramiteAsignar");
            b.AddParameter("@UtilizaTran", IdMesa, SqlDbType.Int);
            b.AddParameter("@IdMesa", IdMesa, SqlDbType.Int);
            b.AddParameter("@IdUsuario", IdUsuario, SqlDbType.Int);
            b.AddParameter("@IdTramite", IdTramite, SqlDbType.Int);
            return b.SelectDataRow();
        }

        public DataTable RechazosGet(int IdMesa)
        {
            b.ExecuteCommandSP("WFO_RecuvaIMSS.dbo.MesasRechazo_Get");
            b.AddParameter("@IdMesa", IdMesa, SqlDbType.Int);
            return b.Select();
        }

        public DataRow ProcesarTramite(int idtramite, int idmesa, int idusuario, int idstatusmesa, string ObsPub, string ObsPrv, string motivosRechazo)
        {
            b.ExecuteCommandSP("WFO_RecuvaIMSS.dbo.spWFOTramiteProcesar");
            b.AddParameter("@UtilizarTran", 1, SqlDbType.SmallInt);
            b.AddParameter("@IdTramite", idtramite, SqlDbType.Int);
            b.AddParameter("@IdMesa", idmesa, SqlDbType.Int);
            b.AddParameter("@IdUsuario", idusuario, SqlDbType.Int);
            b.AddParameter("@IdStatusMesa", idstatusmesa, SqlDbType.Int);
            b.AddParameter("@ObservacionPub", ObsPub, SqlDbType.NVarChar);
            b.AddParameter("@ObservacionPriv", ObsPrv, SqlDbType.NVarChar);
            b.AddParameter("@MotivosRechazo", motivosRechazo, SqlDbType.NVarChar);
            return b.SelectDataRow();
        }
    }
}
