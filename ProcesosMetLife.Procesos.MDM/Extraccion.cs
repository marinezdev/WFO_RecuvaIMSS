using System;
using System.Data;
using OfficeOpenXml;
using System.Data.OleDb;
using System.Configuration;
using System.Data.SqlClient;

namespace ProcesosMetLife.Negocio.Procesos.MDM
{
    public class Extraccion : BD
    {
        public int CreateTramite_onExtraccion()
        {
            return d.extraccion.CreateTramite_onExtraccion();
        }

        /// <summary>
        /// Procedimiento para Importar el archivo de Excel a la base de datos.
        /// </summary>
        /// <param name="rutaArchivo">Indica toda la ruta de archivo de Excel</param>
        public bool ProcesarExcel(string rutaArchivo, string nombreArchivo, string extensionArchivo)
        {
            bool procesado = false;
            string conexion = string.Empty;
            string connExcel03 = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}; Extended Properties = 'Excel 8.0;HDR={1}'";
            string connExcel07 = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}; Extended Properties = 'Excel 8.0;HDR={1}'";
            string isHDR = "Yes";  // Tiene encabezados

            switch (extensionArchivo)
            {
                case "xls":
                    conexion = connExcel03;
                    break;

                case "xlsx":
                    conexion = connExcel07;
                    break;

                default:
                    Console.WriteLine("WARNING. Archivo no Permitido.");
                    break;
            }

            // Generamos la conexión con la base de datos.
            string conStr = String.Format(conexion, rutaArchivo + @"\" + nombreArchivo, isHDR);
            OleDbConnection connExcel = new OleDbConnection(conStr);
            connExcel.Open();

            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dtExcelRead = new DataTable();
            cmdExcel.Connection = connExcel;

            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();

            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dtExcelRead);
            connExcel.Close();

            // NOTE: Al realizar un proceso BULK no se ejecutan los disparadores de la base de datos - ni los contraints.
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conecta_bd"].ConnectionString);
            SqlBulkCopy objbulk = new SqlBulkCopy(con);             //SqlBulkCopy objbulk = new SqlBulkCopy(ConfigurationManager.ConnectionStrings["conecta_bd"].ConnectionString.ToString(), SqlBulkCopyOptions.FireTriggers);
            objbulk.BulkCopyTimeout = 1000;
            objbulk.DestinationTableName = "Extraccion";
            objbulk.ColumnMappings.Add("Matricula", "Matricula");
            objbulk.ColumnMappings.Add("Poliza", "Poliza");
            objbulk.ColumnMappings.Add("Quincena", "Quincena");
            objbulk.ColumnMappings.Add("UnidadPago", "UnidadPago");
            objbulk.ColumnMappings.Add("TipoNomina", "TipoNomina");
            objbulk.ColumnMappings.Add("TipoMovimiento", "TipoMovimiento");
            objbulk.ColumnMappings.Add("Importe", "Importe");
            objbulk.ColumnMappings.Add("PolizaPortal", "PolizaPortal");
            objbulk.ColumnMappings.Add("Archivo", "ArchivoBuscado");

            con.Open();
            objbulk.WriteToServer(dtExcelRead);

            
            //SqlParameter param = new SqlParameter()
            //{
            //    ParameterName = "@IdTramiteTipo",
            //    Value = 1,
            //    //SqlDbType = SqlDbType.SmallInt
            //    //Size = size
            //};

            //SqlCommand cmd = new SqlCommand()
            //{
            //    CommandType = CommandType.Text,
            //    CommandText = "CrearTramites_onExtraccion",
            //    Connection = con,
            //    CommandTimeout = 0
            //};
            //cmd.Parameters.Add(param);
            //cmd.ExecuteReader();



            //b.ExecuteCommandSP("Usuarios_Acceso");
            //b.AddParameter("@clave", clave, SqlDbType.VarChar, 50);
            //b.AddParameter("@contraseña", SuperLogin.Seguridad.Cifrado.Encriptar(contraseña), SqlDbType.VarChar, 100);

            //insertamos los trámites. 


            con.Close();

            return procesado;
        }

        /// <summary>
        /// Agrega un archivo de excel de extracción para ser procesado y agrega un nuevo tramite por cada registro
        /// </summary>
        /// <param name="archivo"></param>
        /// <param name="IdUsuario"></param>
        public void ProcesarExcel(ExcelPackage archivo, int IdUsuario, int Entrega, string NombreArchivo, ref int TotalPolizas)
        {
            DataTable dt = new DataTable();
            dt = Funciones.ManejoExcel.Excel_A_TablaDeDatos(archivo);
            

            //Procesar la tabla

            foreach (DataRow fila in dt.Rows)
            {
                //if (string.IsNullOrEmpty(fila[0].ToString()) && string.IsNullOrEmpty(fila[1].ToString()) && string.IsNullOrEmpty(fila[2].ToString()))
                if (string.IsNullOrEmpty(fila[0].ToString()))
                {
                    // No se encontró la póliza
                    return;
                }

                try
                {
                    int intExtraccion = 0;

                    //intExtraccion = d.extraccion.Agregar(IdUsuario, Entrega, NombreArchivo, fila[0].ToString(), fila[1].ToString(), fila[2].ToString());
                    intExtraccion = d.extraccion.Agregar(IdUsuario, Entrega, NombreArchivo, fila[0].ToString());
                    if (intExtraccion > 0)
                    {
                        Propiedades.TramiteN1 items = new Propiedades.TramiteN1()
                        {
                            Archivo = null             //No utilizado
                        ,
                            NombreArchivo = ""         //No utilizado
                        ,
                            IdTipoArchivo = 2          //1 Concentrado, 2 Extraccion, 3 Carta Promotoria, 4 Carta baja
                        ,
                            IdTipoTramite = 4          //4 MDM
                        ,
                            IdStatus = 1               //Registro
                        ,
                            IdUsuario = IdUsuario      //Usuario Id
                        ,
                            idPrioridad = 5            //1 Supervisor, 2 Sistema, 3 Grandes sumas, 4 Hombres clave, 5 Normal, 6 No procesaro
                        ,
                            Poliza = fila[0].ToString()
                        ,
                            IdPromotoria = 0           //No utilizado
                        ,
                            TipoMovimiento = null      //No utilizado
                        ,
                            UnidadPago = null          //No utilizado
                        ,
                            Quincena = null            //No utilizado
                        ,
                            Importe = null             //No utilizado
                        ,
                            No =  "0" // fila[0].ToString()
                        ,
                            Guid = "000" // fila[2].ToString()

                        };
                        d.tramite.Agregar(items);
                        TotalPolizas += 1;
                    }

                }
                catch (Exception ex)
                {
                    var x = ex.Message;
                }
            }
        }

        public int getEntrega()
        {
            return d.extraccion.getEntrega();
        }

        public bool GuardarCaptura(Propiedades.Extraccion_MDM items)
        {
            //if (d.extraccion.Guardar(items) == 1)
            //{
            //    DataRow dr = d.tramitemesa.ProcesarTramite(items.idtramite, items.idmesa, items.idusuario, items.idstatusmesa, items.obspub, items.obspri, items.motivosrechazo);
            //    if (!dr[1].ToString().Contains("Trámite Procesado"))
            //        return false;
            //    else
            //        return true;
            //}
            //else
                return false;
        }

        public DataSet ExportarAExcel(int IdEntrega)
        {
            return d.extraccion.ExportarAExcel(IdEntrega);            
        }

        public DataSet getCapturaValidacion(int IdTramite)
        {
            return d.extraccion.getCapturaValidacion(IdTramite);
        }
    }
}
