using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcesosMetLife.AccesoDatos.MDM.Tablas
{
    public class Extraccion
    {
        internal ManejoDatos b { get; set; } = new ManejoDatos();

        /// <summary>
        /// Agrega los registros de una extración de un archivo
        /// </summary>
        /// <param name="IdUsuario"></param>
        /// <param name="prms"></param>
        /// <returns></returns>
        public int Agregar(int IdUsuario, int Entrega, string NombreArchivo, params string[] prms)
        {
            //b.ExecuteCommandSP("WFO_RecuvaIMSS.dbo.Movimientos_Extraccion_Add");
            //b.AddParameter("@idusuario", IdUsuario, SqlDbType.Int);
            //b.AddParameter("@no", prms[0], SqlDbType.NVarChar);
            //b.AddParameter("@poliza", prms[1], SqlDbType.NVarChar);
            //b.AddParameter("@guid", prms[2], SqlDbType.NVarChar);
            //b.AddParameter("@entrega", Entrega, SqlDbType.Int);
            //b.AddParameter("@Archivo", NombreArchivo, SqlDbType.NVarChar);

            b.ExecuteCommandSP("WFO_RecuvaIMSS.dbo.Movimientos_Extraccion_Add");
            b.AddParameter("@idusuario", IdUsuario, SqlDbType.Int);
            b.AddParameter("@no", "0", SqlDbType.NVarChar);
            b.AddParameter("@poliza", prms[0], SqlDbType.NVarChar);
            b.AddParameter("@guid", "000", SqlDbType.NVarChar);
            b.AddParameter("@entrega", Entrega, SqlDbType.Int);
            b.AddParameter("@Archivo", NombreArchivo, SqlDbType.NVarChar);

            return b.InsertUpdateDelete();
        }

        /// <summary>
        /// Actualiza los datos de un trámite con la información capturada
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public int Guardar(Propiedades.Extraccion_MDM items)
        {
            b.ExecuteCommandQuery("UPDATE WFO_RecuvaIMSS.dbo.extraccion SET " +
            "PaisNacimiento                     = @paisnacimiento " +
            ",EstadoNacimiento                  = @estadonacimiento " +
            ",Ciudad                            = @ciudad " +
            ",Nacionalidad                      = @nacionalidad " +
            ",Ocupacion                         = @ocupacion " +
            ",ClaveOcupacion                    = @claveocupacion " +
            ",DetalleOcupacion                  = @detalleocupacion " +
            ",IngresoMensual                    = @ingresomensual " +
            ",TransaccionesAnualesAportaciones  = @transaccionesanualesaportaciones " +
            ",TransaccionesAnualesRetiros       = @transaccionesanualesretiros " +
            ",TransaccionesAportaciones         = @transaccionesaportaciones " +
            ",TransaccionesRetiros              = @transaccionesretiros " +
            ",PagoImpuestosExtranjero           = @pagoimpuestosextranjero " +
            ",PagoImpuestosExtranjeroPais       = @pagoimpuestosextranjeropais " +
            ",NSS                               = @nss " +
            ",DesempeñoDestacado                = @desempeñodestacado " +
            ",RazonesContratacion               = @razonescontratacion " +
            ",NivelRiesgo                       = @nivelriesgo " +
            ",LimitarDivulgacion                = @limitardivulgacion " +
            ",Tipodocumento                     = @tipodocumento " +
            ",SubtipoDocumento                  = @subtipodocumento " +
            ",Referencia                        = @referencia " +
            ",FechaEmision                      = @fechaemision " +
            ",FechaVigencia                     = @fechavigencia " +
            ",EntidadGubernamentalEmisora       = @entidadgubernamentalemisora " +
            ",PaisEmisor                        = @paisemisor " +
            ",Contador                          = @contador " +
            ",Eliminar                          = @eliminar " +
            ",UsuarioCaptura1                   = @usuariocaptura1 " +
            "WHERE Id=@id");
            b.AddParameter("@paisnacimiento",                   items.PaisNacimiento,                   SqlDbType.NVarChar);
            b.AddParameter("@estadonacimiento",                 items.EstadoNacimiento,                 SqlDbType.NVarChar);
            b.AddParameter("@ciudad",                           items.Ciudad,                           SqlDbType.NVarChar);
            b.AddParameter("@nacionalidad",                     items.Nacionalidad,                     SqlDbType.NVarChar);
            b.AddParameter("@ocupacion",                        items.Ocupacion,                        SqlDbType.NVarChar);
            b.AddParameter("@claveocupacion",                   items.ClaveOcupacion,                   SqlDbType.NVarChar);
            b.AddParameter("@detalleocupacion",                 items.DetalleOcupacion,                 SqlDbType.NVarChar);
            b.AddParameter("@ingresomensual",                   items.IngresoMensual,                   SqlDbType.NVarChar);
            b.AddParameter("@transaccionesanualesaportaciones", items.TransaccionesAnualesAportaciones, SqlDbType.NVarChar);
            b.AddParameter("@transaccionesanualesretiros",      items.TransaccionesAnualesRetiros,      SqlDbType.NVarChar);
            b.AddParameter("@transaccionesaportaciones",        items.TransaccionesAportaciones,        SqlDbType.NVarChar);
            b.AddParameter("@transaccionesretiros",             items.TransaccionesRetiros,             SqlDbType.NVarChar);
            b.AddParameter("@pagoimpuestosextranjero",          items.PagoImpuestosExtranjero,          SqlDbType.NVarChar);
            b.AddParameter("@pagoimpuestosextranjeropais",      items.PagoImpuestosExtranjeroPais,      SqlDbType.NVarChar);
            b.AddParameter("@nss",                              items.NSS,                              SqlDbType.NVarChar);
            b.AddParameter("@desempeñodestacado",               items.DesempeñoDestacado,               SqlDbType.NVarChar);
            b.AddParameter("@razonescontratacion",              items.RazonesContratacion,              SqlDbType.NVarChar);
            b.AddParameter("@nivelriesgo",                      items.NivelRiesgo,                      SqlDbType.NVarChar);
            b.AddParameter("@limitardivulgacion",               items.LimitarDivulgacion,               SqlDbType.NVarChar);
            b.AddParameter("@tipodocumento",                    items.Tipodocumento,                    SqlDbType.NVarChar);
            b.AddParameter("@subtipodocumento",                 items.SubtipoDocumento,                 SqlDbType.NVarChar);
            b.AddParameter("@referencia",                       items.Referencia,                       SqlDbType.NVarChar);
            b.AddParameter("@fechaemision",                     items.FechaEmision,                     SqlDbType.NVarChar);
            b.AddParameter("@fechavigencia",                    items.FechaVigencia,                    SqlDbType.NVarChar);
            b.AddParameter("@entidadgubernamentalemisora",      items.EntidadGubernamentalEmisora,      SqlDbType.NVarChar);
            b.AddParameter("@paisemisor",                       items.PaisEmisor,                       SqlDbType.NVarChar);
            b.AddParameter("@contador",                         items.Contador,                         SqlDbType.NVarChar);
            b.AddParameter("@eliminar",                         items.Eliminar,                         SqlDbType.NVarChar);
            b.AddParameter("@usuariocaptura1",                  items.idusuario,                        SqlDbType.Int);
            b.AddParameter("@id",                               items.Id,                               SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        public int CreateTramite_onExtraccion()
        {
            int _resultado = -1;
            b.ExecuteCommandSP("WFO_RecuvaIMSS.dbo.CrearTramites_onExtraccion");
            b.AddParameter("@IdTramiteTipo", 1, SqlDbType.SmallInt);
            b.Select();
            return _resultado;
        }

        public int getEntrega()
        {
            int _resultado = -1;
            b.ExecuteCommandSP("WFO_RecuvaIMSS.dbo.Extraccion_getEntrega");
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                _resultado = int.Parse(reader["Entrega"].ToString());
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return _resultado;
        }

        public DataSet getCapturaValidacion(int IdTramite) 
        {
            string consulta = "";
            consulta += "SELECT Id, Fecha, IdUsuario, Numero, Poliza, GUID_, ISNULL((SELECT WFO_RecuvaIMSS.dbo.Pais.Id FROM WFO_RecuvaIMSS.dbo.Pais WHERE WFO_RecuvaIMSS.dbo.Pais.Nombre = PaisNacimiento),0) AS IdPais, PaisNacimiento, ISNULL((SELECT WFO_RecuvaIMSS.dbo.EstadoProvincia.Id FROM WFO_RecuvaIMSS.dbo.EstadoProvincia WHERE WFO_RecuvaIMSS.dbo.EstadoProvincia.Nombre = EstadoNacimiento),0) AS IdEstadoNacimiento, EstadoNacimiento, Ciudad, ISNULL((SELECT WFO_RecuvaIMSS.dbo.Pais.Id FROM WFO_RecuvaIMSS.dbo.Pais WHERE WFO_RecuvaIMSS.dbo.Pais.Nombre = PaisNacimiento),0) AS IdNacionalidad, Nacionalidad, ISNULL((SELECT WFO_RecuvaIMSS.dbo.OcupacionProfesional.Id FROM WFO_RecuvaIMSS.dbo.OcupacionProfesional WHERE WFO_RecuvaIMSS.dbo.OcupacionProfesional.Nombre = Ocupacion),0) AS IdOcupacion, Ocupacion, ISNULL((SELECT MIN(WFO_RecuvaIMSS.dbo.OcupacionProfesionalClave.Id) FROM WFO_RecuvaIMSS.dbo.OcupacionProfesionalClave WHERE WFO_RecuvaIMSS.dbo.OcupacionProfesionalClave.Nombre = ClaveOcupacion),0) AS IdClaveOcupacion, ClaveOcupacion, DetalleOcupacion, IngresoMensual, TransaccionesAnualesAportaciones, TransaccionesAnualesRetiros, TransaccionesAportaciones, TransaccionesRetiros, PagoImpuestosExtranjero, PagoImpuestosExtranjeroPais, NSS, DesempeñoDestacado, RazonesContratacion, NivelRiesgo, LimitarDivulgacion, ISNULL((SELECT WFO_RecuvaIMSS.dbo.TipoDocumento.Id FROM WFO_RecuvaIMSS.dbo.TipoDocumento WHERE WFO_RecuvaIMSS.dbo.TipoDocumento.Nombre = Tipodocumento),0) AS IdTipodocumento, Tipodocumento, ISNULL((SELECT WFO_RecuvaIMSS.dbo.SubTipoDocumento.Id FROM WFO_RecuvaIMSS.dbo.SubTipoDocumento WHERE WFO_RecuvaIMSS.dbo.SubTipoDocumento.Nombre = SubtipoDocumento),0) AS IdSubtipoDocumento, SubtipoDocumento, Referencia, FechaEmision, FechaVigencia, ISNULL((SELECT WFO_RecuvaIMSS.dbo.EntidadGubernamentalEmisora.Id FROM WFO_RecuvaIMSS.dbo.EntidadGubernamentalEmisora WHERE MWFO_RecuvaIMSSDM.dbo.EntidadGubernamentalEmisora.Nombre = EntidadGubernamentalEmisora),0) AS IdEntidadGubernamentalEmisora, EntidadGubernamentalEmisora, ISNULL((SELECT WFO_RecuvaIMSS.dbo.Pais.Id FROM WFO_RecuvaIMSS.dbo.Pais WHERE WFO_RecuvaIMSS.dbo.Pais.Nombre = PaisNacimiento),0) AS IdPaisEmisor, PaisEmisor, Contador, Eliminar, UsuarioCaptura1, UsuarioCaptura2, EstadoFinal, Comentarios FROM WFO_RecuvaIMSS.DBO.CAPTURA1 WHERE POLIZA = (SELECT POLIZA FROM TRAMITE_DET_MDM WHERE IDTRAMITE = @IdTramite);";
            consulta += "SELECT Id, Fecha, IdUsuario, Numero, Poliza, GUID_, ISNULL((SELECT WFO_RecuvaIMSS.dbo.Pais.Id FROM WFO_RecuvaIMSS.dbo.Pais WHERE WFO_RecuvaIMSS.dbo.Pais.Nombre = PaisNacimiento),0) AS IdPais, PaisNacimiento, ISNULL((SELECT WFO_RecuvaIMSS.dbo.EstadoProvincia.Id FROM WFO_RecuvaIMSS.dbo.EstadoProvincia WHERE WFO_RecuvaIMSS.dbo.EstadoProvincia.Nombre = EstadoNacimiento),0) AS IdEstadoNacimiento, EstadoNacimiento, Ciudad, ISNULL((SELECT WFO_RecuvaIMSS.dbo.Pais.Id FROM WFO_RecuvaIMSS.dbo.Pais WHERE WFO_RecuvaIMSS.dbo.Pais.Nombre = PaisNacimiento),0) AS IdNacionalidad, Nacionalidad, ISNULL((SELECT WFO_RecuvaIMSS.dbo.OcupacionProfesional.Id FROM WFO_RecuvaIMSS.dbo.OcupacionProfesional WHERE WFO_RecuvaIMSS.dbo.OcupacionProfesional.Nombre = Ocupacion),0) AS IdOcupacion, Ocupacion, ISNULL((SELECT MIN(WFO_RecuvaIMSS.dbo.OcupacionProfesionalClave.Id) FROM WFO_RecuvaIMSS.dbo.OcupacionProfesionalClave WHERE WFO_RecuvaIMSS.dbo.OcupacionProfesionalClave.Nombre = ClaveOcupacion),0) AS IdClaveOcupacion, ClaveOcupacion, DetalleOcupacion, IngresoMensual, TransaccionesAnualesAportaciones, TransaccionesAnualesRetiros, TransaccionesAportaciones, TransaccionesRetiros, PagoImpuestosExtranjero, PagoImpuestosExtranjeroPais, NSS, DesempeñoDestacado, RazonesContratacion, NivelRiesgo, LimitarDivulgacion, ISNULL((SELECT WFO_RecuvaIMSS.dbo.TipoDocumento.Id FROM WFO_RecuvaIMSS.dbo.TipoDocumento WHERE WFO_RecuvaIMSS.dbo.TipoDocumento.Nombre = Tipodocumento),0) AS IdTipodocumento, Tipodocumento, ISNULL((SELECT WFO_RecuvaIMSS.dbo.SubTipoDocumento.Id FROM WFO_RecuvaIMSS.dbo.SubTipoDocumento WHERE WFO_RecuvaIMSS.dbo.SubTipoDocumento.Nombre = SubtipoDocumento),0) AS IdSubtipoDocumento, SubtipoDocumento, Referencia, FechaEmision, FechaVigencia, ISNULL((SELECT WFO_RecuvaIMSS.dbo.EntidadGubernamentalEmisora.Id FROM WFO_RecuvaIMSS.dbo.EntidadGubernamentalEmisora WHERE WFO_RecuvaIMSS.dbo.EntidadGubernamentalEmisora.Nombre = EntidadGubernamentalEmisora),0) AS IdEntidadGubernamentalEmisora, EntidadGubernamentalEmisora, ISNULL((SELECT WFO_RecuvaIMSS.dbo.Pais.Id FROM WFO_RecuvaIMSS.dbo.Pais WHERE WFO_RecuvaIMSS.dbo.Pais.Nombre = PaisNacimiento),0) AS IdPaisEmisor, PaisEmisor, Contador, Eliminar, UsuarioCaptura1, UsuarioCaptura2, EstadoFinal, Comentarios FROM WFO_RecuvaIMSS.DBO.CAPTURA2 WHERE POLIZA = (SELECT POLIZA FROM TRAMITE_DET_MDM WHERE IDTRAMITE = @IdTramite);";
            consulta += "SELECT * FROM WFO_RecuvaIMSS.DBO.CapturaValidacion WHERE POLIZA = (SELECT POLIZA FROM TRAMITE_DET_MDM WHERE IDTRAMITE = @IdTramite);";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@IdTramite", IdTramite, SqlDbType.Int);
            return b.SelectExecuteFunctions();
        }

        public DataSet ExportarAExcel(int IdEntrega)
        {
            string consulta = "";
            consulta += " SELECT  ";
            consulta += "  Poliza ";
            consulta += "  , IIF((PaisNacimiento = '( SIN INFORMACIÓN )') OR (PaisNacimiento = 'S/N'), '', PaisNacimiento) AS[Lugar de Nacimiento / País] ";
            consulta += "   , IIF((EstadoNacimiento = '( SIN INFORMACIÓN )') OR (EstadoNacimiento = 'S/N'), '', EstadoNacimiento)  AS[Lugar De Nacimiento - Estado / Provincia] ";
            consulta += "   , IIF((Ciudad = '( SIN INFORMACIÓN )') OR (Ciudad = 'S/N'), '', Ciudad) AS[Ciudad / Población] ";
            consulta += "   , IIF((Nacionalidad = '( SIN INFORMACIÓN )') OR (Nacionalidad = 'S/N'), '', Nacionalidad) AS Nacionalidad ";
            consulta += "   , IIF ((Ocupacion = '( SIN INFORMACIÓN )') OR (Ocupacion = 'S/N'), '', Ocupacion) AS[Ocupacion ó Profesion] ";
            consulta += "   , IIF((ClaveOcupacion = '( SIN INFORMACIÓN )') OR (ClaveOcupacion = 'S/N'), '', ClaveOcupacion) AS[Clave De Ocupación ó Profesion] ";
            consulta += "   , IIF((DetalleOcupacion = '( SIN INFORMACIÓN )') OR (DetalleOcupacion = 'S/N'), '', DetalleOcupacion) AS[Detalle De Ocupacion ó Profesion] ";
            consulta += "   , IIF((IngresoMensual = '( SIN INFORMACIÓN )') OR (IngresoMensual = 'S/N'), '', REPLACE(IngresoMensual, ',', '')) AS[Ingreso Mensual aproximado(pesos)] ";
            consulta += "   , IIF((TransaccionesAnualesAportaciones = '( SIN INFORMACIÓN )') OR (TransaccionesAnualesAportaciones = 'S/N'), '', TransaccionesAnualesAportaciones)  AS[# Aproximado De Transacciones Anuales - Aportaciones]    ";
            consulta += "   , IIF((TransaccionesAnualesRetiros = '( SIN INFORMACIÓN )') OR (TransaccionesAnualesRetiros = 'S/N'), '', TransaccionesAnualesRetiros)  AS[# Aproximado De Transacciones Anuales - Retiros]   ";
            consulta += "   , IIF((TransaccionesAportaciones = '( SIN INFORMACIÓN )') OR (TransaccionesAportaciones = 'S/N'), '', REPLACE(TransaccionesAportaciones, ',', ''))  AS[Monto Aproximado De Transacciones - Aportaciones] ";
            consulta += " , IIF((TransaccionesRetiros = '( SIN INFORMACIÓN )') OR (TransaccionesRetiros = 'S/N'), '', REPLACE(TransaccionesRetiros, ',', ''))  AS[Monto Aproximado De Transacciones - Retiros] ";
            consulta += " , IIF((PagoImpuestosExtranjero = '-1'), '', (SELECT Nombre FROM preguntas where id = PagoImpuestosExtranjero) ) AS[¿Esta Usted Sujeto al Pago de Impuestos en el Extranjero ?] ";
            //consulta += "   , IIF ( (PagoImpuestosExtranjeroPais = '-1'), '', (SELECT Nombre FROM preguntas WHERE Id = PagoImpuestosExtranjeroPais) ) AS [Pago de Impuestos en el Extranjero - Pais]    ";
            consulta += " , IIF((DesempeñoDestacado = '-1'), '', (SELECT Nombre FROM preguntas where id = DesempeñoDestacado) ) AS[¿Desempeña o ha desempeñado Usted, su Cónyuge ó un Familiar Funciones Públicas Destacadas ?] ";
            consulta += "   , IIF((RazonesContratacion = '-1'), '', (SELECT Nombre FROM preguntas WHERE Id = RazonesContratacion) ) AS[Especificar las razones por las cuales es de tu interés la contratación de un seguro en territorionacional] ";
            consulta += "   , IIF((NivelRiesgo = '( SIN INFORMACIÓN )') OR (NivelRiesgo = 'S/N'), '', NivelRiesgo)  AS[Nivel De Riesgo] ";
            consulta += "   , IIF((LimitarDivulgacion = '-1'), '', (SELECT Nombre FROM preguntas where id = LimitarDivulgacion) ) AS[Limitar el Uso o Divulgación o Transferencia de Datos] ";
            consulta += "   , IIF((Tipodocumento = '( SIN INFORMACIÓN )') OR (Tipodocumento = 'S/N'), '', Tipodocumento)  AS[Tipo De Documento] ";
            consulta += "   , IIF((SubtipoDocumento = '( SIN INFORMACIÓN )') OR (SubtipoDocumento = 'S/N'), '', SubtipoDocumento)  AS[Subtipo de documento] ";
            consulta += "   , IIF((Referencia = '( SIN INFORMACIÓN )') OR (Referencia = 'S/N'), '', Referencia)  AS[Referencia Del Documento] ";
            consulta += "   , IIF((FechaEmision = '( SIN INFORMACIÓN )') OR (FechaEmision = 'S/N') OR (FechaEmision = '01/01/3000') OR (FechaEmision = '01/01/1900'), '', FechaEmision)  AS[Fecha De Emisión] ";
            consulta += "   , IIF((FechaVigencia = '( SIN INFORMACIÓN )') OR (FechaVigencia = 'S/N') OR (FechaVigencia = '01/01/3000') OR (FechaVigencia = '01/01/1900'), '', FechaVigencia)  AS[Fecha De Vigencia] ";
            consulta += "   , IIF((EntidadGubernamentalEmisora = '( SIN INFORMACIÓN )') OR (EntidadGubernamentalEmisora = 'S/N'), '', EntidadGubernamentalEmisora)  AS[Entidad Gubernamental Emisora] ";
            consulta += "   , IIF((PaisEmisor = '( SIN INFORMACIÓN )') OR (PaisEmisor = 'S/N'), '', PaisEmisor)  AS[Pais Emisor] ";
            consulta += "   , '' AS Nacionalidad2 ";
            consulta += "   , '' AS Nacionalidad3 ";
            consulta += "   , IIF( (EstadoFinal = '-1'), '', (SELECT Nombre FROM EstadoFinal WHERE id = EstadoFinal)) AS 'Estado Final' ";
            consulta += "   , IIF((Comentarios = '-1'), '', (SELECT Nombre FROM Comentarios WHERE Id = Comentarios)) AS 'Comentarios' ";
            consulta += "   , (SELECT REPLACE(Tramite_Mesa.ObservacionPrivada, 'Trámite sin proceso de calidad.', '') FROM Tramite_Mesa WHERE Tramite_Mesa.Id = (SELECT MAX(Tramite_Mesa.Id) FROM Tramite_Mesa WHERE Tramite_Mesa.IdTramite = (SELECT Tramite_Det_MDM.IdTramite FROM Tramite_Det_MDM WHERE Tramite_Det_MDM.Poliza = captura1.Poliza AND Tramite_Det_MDM.No = captura1.Numero AND Tramite_Det_MDM.[Guid] = captura1.GUID_))) AS Observaciones    ";
            //  				--, NSS AS[Numero de Seguridad Social ó Numero de Identificacion de Impuestos]    ";
            consulta += " FROM WFO_RecuvaIMSS.dbo.Captura1 ";
            consulta += " WHERE WFO_RecuvaIMSS.dbo.Captura1.Poliza IN (";
            consulta += "       SELECT Extraccion.Poliza ";
            consulta += "       FROM Extraccion ";
            consulta += "       WHERE Extraccion.Entrega = " + IdEntrega.ToString();
            consulta += " ) ";
            //consulta += " order by Poliza desc";
            //consulta += " AND NOT WFO_RecuvaIMSS.dbo.Captura1.Poliza IN (";
            //consulta += "       SELECT MDMValidationCaptura.poliza ";
            //consulta += "       FROM MDMValidationCaptura";
            //consulta += " )";

            b.ExecuteCommandQuery(consulta);
            //b.AddParameter("@entrega", IdEntrega, SqlDbType.Int);
            return b.SelectExecuteFunctions();
        }
    }
}