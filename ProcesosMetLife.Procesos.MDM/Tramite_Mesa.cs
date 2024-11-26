using System;
using System.Collections.Generic;
using System.Data;

namespace ProcesosMetLife.Negocio.Procesos.MDM
{
    public class Tramite_Mesa : BD
    {
        public DataRow AsignarTramite(int IdMesa, int IdUsuario, int IdTramite)
        {
            return  d.tramitemesa.AsignarTramite(IdMesa, IdUsuario, IdTramite);
        }

        public DataTable RechazosGet(int IdMesa)
        {
            return d.tramitemesa.RechazosGet(IdMesa);
        }

        public string ProcesarTramite(int idtramite, int idmesa, int idusuario, int idstatusmesa, string obsPub, string ObsPrv, List<int> motivosRechazo)
        {
            string claveMotivos = "";

            if (motivosRechazo != null)
            {
                foreach (int motivo in motivosRechazo)
                {
                    //if (claveMotivos.Length > 0)
                        //claveMotivos += ",";

                    claveMotivos += motivo.ToString() + ",";
                }
            }

            var x = d.tramitemesa.ProcesarTramite(idtramite, idmesa, idusuario, idstatusmesa, obsPub, ObsPrv, claveMotivos);
            return x[0].ToString() + ":" + x[1].ToString();
        }
    }
}