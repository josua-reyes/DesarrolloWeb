using CapaDatos;
using CapaModelos.DTO;
using CapaModelos.Modelos;

namespace CapaLogicaNegocio
{
    public class SegundoParcialBll
    {
        SegundoParcialDal _segundoParcialDal;
        public SegundoParcialBll()
        {
            _segundoParcialDal = new SegundoParcialDal();

        }

        public GuardarFacturaVentaRespuesta GuardarFacturaVenta(GuardarFacturaVentaSolicitud guardarFacturaVentaSolicitud)
        {
            return _segundoParcialDal.GuardarFacturaVenta(guardarFacturaVentaSolicitud);
        }

        public ResultadoConsultaDatos ObtenerProductosPorIdProveedor(int idProveedor)
        {
            return _segundoParcialDal.ObtenerProductosPorIdProveedor(idProveedor);
        }

        public ResultadoConsultaDatos ObtenerCategorias()
        {
            return _segundoParcialDal.ObtenerCategorias();
        }

        public ResultadoConsultaDatos ObtenerClientesPorIdProductoVenta(int idProducto)
        {
            return _segundoParcialDal.ObtenerClientesPorIdProductoVenta(idProducto);
        }
    }
}
