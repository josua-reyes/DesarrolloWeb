using CapaModelos.DTO;
using CapaModelos.Modelos;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Data;
using System.IO;

namespace CapaDatos
{
    public class SegundoParcialDal : ConexionBaseDatos
    {
        public SegundoParcialDal()
        {

        }
        public ResultadoConsultaDatos EjecutarConsulta(string consultaSql)
        {
            ResultadoConsultaDatos resultadoConsultaDatos = new ResultadoConsultaDatos();

            try
            {
                using (_Connection = new OracleConnection(_CadenaConexion))
                {
                    _Connection.Open();

                    using (OracleCommand command = new OracleCommand(consultaSql, _Connection))
                    {
                        command.CommandType = CommandType.Text;

                        using (OracleDataAdapter adapter = new OracleDataAdapter(command))
                        {
                            adapter.Fill(resultadoConsultaDatos.Datos);
                        }
                    }
                }
                DataSet ds = new DataSet();
                ds.Tables.Add(resultadoConsultaDatos.Datos);

                using (StringWriter sw = new StringWriter())
                {
                    ds.WriteXml(sw);
                    resultadoConsultaDatos.XmlDatos = sw.ToString();
                }

                resultadoConsultaDatos.Estado = "EXITO";
            }
            catch (Exception ex)
            {
                resultadoConsultaDatos.Estado = "ERROR";
                resultadoConsultaDatos.DescripcionError = ex.Message;
            }
            finally
            {
                // La conexión ya se libera automáticamente al salir del bloque
                // `using`, por lo que no es necesario cerrarla manualmente
                // y evitamos posibles excepciones por haber sido ya
                // descartada.
            }

            return resultadoConsultaDatos;
        }

        public GuardarFacturaVentaRespuesta GuardarFacturaVenta(GuardarFacturaVentaSolicitud guardarFacturaVentaSolicitud)
        {
            GuardarFacturaVentaRespuesta guardarFacturaVentaRespuesta = new GuardarFacturaVentaRespuesta();

            try
            {
                using (_Connection = new OracleConnection(_CadenaConexion))
                {
                    _Connection.Open();
                    try
                    {
                        _Transaction = _Connection.BeginTransaction();

                        using (_Command = new OracleCommand("SegundoParcial.InsertarFactura", _Connection))
                        {
                            _Command.Transaction = _Transaction;
                            _Command.CommandType = CommandType.StoredProcedure;

                            _Command.Parameters.Add("p_Fecha", OracleDbType.Date).Value = guardarFacturaVentaSolicitud.Factura.Fecha;
                            _Command.Parameters.Add("p_IdCliente", OracleDbType.Int32).Value = guardarFacturaVentaSolicitud.Factura.IdCliente;
                            _Command.Parameters.Add("p_NumeroFactura", OracleDbType.Int32).Direction = ParameterDirection.Output;
                            _Command.Parameters.Add("p_Estado", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output;
                            _Command.Parameters.Add("p_DescripcionError", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;

                            _Command.ExecuteNonQuery();

                            guardarFacturaVentaRespuesta.Estado = _Command.Parameters["p_Estado"].Value.ToString();
                            guardarFacturaVentaRespuesta.DescripcionError = _Command.Parameters["p_DescripcionError"].Value.ToString();

                            if (!guardarFacturaVentaRespuesta.Estado.Contains("EXITO"))
                            {
                                throw new Exception(guardarFacturaVentaRespuesta.DescripcionError);
                            }

                            guardarFacturaVentaRespuesta.NumeroFactura = ((OracleDecimal)_Command.Parameters["p_NumeroFactura"].Value).ToInt32();

                            foreach (Venta venta in guardarFacturaVentaSolicitud.Factura.DetalleVentas)
                            {
                                guardarFacturaVentaRespuesta = InsertarVenta(venta, guardarFacturaVentaRespuesta.NumeroFactura, _Transaction);

                                if (!guardarFacturaVentaRespuesta.Estado.Contains("EXITO"))
                                {
                                    throw new Exception(guardarFacturaVentaRespuesta.DescripcionError);
                                }
                            }
                            guardarFacturaVentaRespuesta.CantidadDetalleVenta = guardarFacturaVentaSolicitud.Factura.DetalleVentas.Count;
                        }
                        _Transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        _Transaction.Rollback();
                        guardarFacturaVentaRespuesta.Estado = "ERROR";
                        guardarFacturaVentaRespuesta.DescripcionError = ex.Message;
                        guardarFacturaVentaRespuesta.NumeroFactura = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                guardarFacturaVentaRespuesta.Estado = "ERROR";
                guardarFacturaVentaRespuesta.DescripcionError = ex.Message;
                guardarFacturaVentaRespuesta.NumeroFactura = 0;

            }
            finally
            {
                // La conexión se cierra automáticamente al finalizar el blo
                // `using`; no es necesario invocar Close nuevamente.
            }
            return guardarFacturaVentaRespuesta;
        }

        private GuardarFacturaVentaRespuesta InsertarVenta(Venta venta, int numeroFactura, OracleTransaction transaction)
        {
            GuardarFacturaVentaRespuesta guardarFacturaVentaRespuesta = new GuardarFacturaVentaRespuesta();
            OracleCommand command;

            try
            {
                using (command = new OracleCommand("SegundoParcial.InsertarVenta", transaction.Connection))
                {
                    command.Transaction = transaction;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("p_IdFactura", OracleDbType.Int32).Value = numeroFactura;
                    command.Parameters.Add("p_IdProducto", OracleDbType.Int32).Value = venta.IdProducto;
                    command.Parameters.Add("p_Cantidad", OracleDbType.Int32).Value = venta.Cantidad;
                    command.Parameters.Add("p_Estado", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output;
                    command.Parameters.Add("p_DescripcionError", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;

                    command.ExecuteNonQuery();

                    guardarFacturaVentaRespuesta.Estado = command.Parameters["p_Estado"].Value.ToString();
                    guardarFacturaVentaRespuesta.DescripcionError = command.Parameters["p_DescripcionError"].Value.ToString();
                    guardarFacturaVentaRespuesta.NumeroFactura = numeroFactura;
                }
            }
            catch (Exception ex)
            {
                guardarFacturaVentaRespuesta.Estado = "ERROR";
                guardarFacturaVentaRespuesta.DescripcionError = ex.Message;
            }

            return guardarFacturaVentaRespuesta;
        }

        public ResultadoConsultaDatos ObtenerProductosPorIdProveedor(int idProveedor)
        {
            string consultaSql = $@"
                    SELECT
                        PROD.ID_PROVEEDOR,
                        PROV.NOMBRE AS NOMBRE_PROVEEDOR,
                        PROD.ID_PRODUCTO,
                        PROD.DESCRIPCION AS NOMBRE_PRODUCTO,
                        PROD.PRECIO,
                        CAT.DESCRIPCION AS NOMBRE_CATEGORIA
                    FROM PRODUCTOS PROD
                    INNER JOIN PROVEEDORES PROV
                        ON PROV.ID_PROVEEDOR = PROD.ID_PROVEEDOR
                    INNER JOIN CATEGORIAS CAT
                        ON CAT.ID_CATEGORIA = PROD.ID_CATEGORIA
                    WHERE PROD.ID_PROVEEDOR = {idProveedor}";

            return EjecutarConsulta(consultaSql);
        }

        public ResultadoConsultaDatos ObtenerCategorias()
        {
            string consultaSql = $@"
                    SELECT
                      CAT.ID_CATEGORIA,
                      CAT.DESCRIPCION AS NOMBRE_CATEGORIA,
                      PROD.ID_PRODUCTO,
                      PROD.DESCRIPCION AS NOMBRE_PRODUCTO,
                      PROD.PRECIO
                    FROM CATEGORIAS CAT
                    INNER JOIN PRODUCTOS PROD
                      ON PROD.ID_CATEGORIA = CAT.ID_CATEGORIA";

            return EjecutarConsulta(consultaSql);
        }

        public ResultadoConsultaDatos ObtenerClientesPorIdProductoVenta(int idProducto)
        {
            string consultaSql = $@"
                    SELECT
                      FAC.ID_FACTURA,
                      FAC.FECHA,
                      CLI.ID_CLIENTE,
                      CLI.NOMBRE AS NOMBRE_CLIENTE,
                      VEN.ID_PRODUCTO,
                      PROD.DESCRIPCION AS NOMBRE_PRODUCTO,
                      VEN.CANTIDAD
                    FROM VENTAS VEN
                    INNER JOIN FACTURAS FAC
                      ON FAC.ID_FACTURA = VEN.ID_FACTURA
                    INNER JOIN CLIENTES CLI
                      ON CLI.ID_CLIENTE = FAC.ID_CLIENTE
                    INNER JOIN PRODUCTOS PROD
                      ON PROD.ID_PRODUCTO = VEN.ID_PRODUCTO
                    WHERE VEN.ID_PRODUCTO = {idProducto}";

            return EjecutarConsulta(consultaSql);
        }

    }
}
