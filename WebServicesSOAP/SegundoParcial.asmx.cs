using CapaLogicaNegocio;
using CapaModelos.DTO;
using CapaModelos.Modelos;
using System.Web.Services;
using System.Xml;

namespace WebServicesSOAP
{
    /// <summary>
    /// Descripción breve de SegundoParcial
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]

    public class SegundoParcial : System.Web.Services.WebService
    {
        SegundoParcialBll _segundoParcialBll;

        [WebMethod]
        public GuardarFacturaVentaRespuesta GuardarFacturaVenta(GuardarFacturaVentaSolicitud guardarFacturaVentaSolicitud)
        {
            _segundoParcialBll = new SegundoParcialBll();
            return _segundoParcialBll.GuardarFacturaVenta(guardarFacturaVentaSolicitud);
        }

        [WebMethod]
        public XmlDocument ObtenerProductosPorIdProveedor(int idProveedor)
        {
            _segundoParcialBll = new SegundoParcialBll();
            return _segundoParcialBll.ObtenerProductosPorIdProveedor(idProveedor).ObtenerDocumentoXML();
        }

        [WebMethod]
        public XmlDocument ObtenerCategorias()
        {
            _segundoParcialBll = new SegundoParcialBll();
            return _segundoParcialBll.ObtenerCategorias().ObtenerDocumentoXML();
        }

        [WebMethod]
        public XmlDocument ObtenerClientesPorIdProductoVenta(int idProducto)
        {
            _segundoParcialBll = new SegundoParcialBll();
            return _segundoParcialBll.ObtenerClientesPorIdProductoVenta(idProducto).ObtenerDocumentoXML();
        }
    }
}
