using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Xml;
using System.Xml.Linq;
using WebServicesSOAP;

namespace WebApiREST.Controllers
{
    [ApiController]
    public class SegundoParcialController : ControllerBase
    {
        private readonly ILogger<SegundoParcialController> _logger;
        private readonly IConfiguration _configuration;
        private readonly string _urlWebServicesSOAP;
        private readonly SegundoParcialSoapClient _service;

        public SegundoParcialController(ILogger<SegundoParcialController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _urlWebServicesSOAP = _configuration.GetSection("UrlWebServicesSOAP").Value;
            _service = new(SegundoParcialSoapClient.EndpointConfiguration.SegundoParcialSoap, _urlWebServicesSOAP);
        }

        [HttpPost]
        [Produces("application/json")]
        [Route("/api/SegundoParcial/GuardarFacturaVenta")]
        public GuardarFacturaVentaRespuesta GuardarFacturaVenta(GuardarFacturaVentaSolicitud guardarFacturaVentaSolicitud)
        {
            return _service.GuardarFacturaVenta(guardarFacturaVentaSolicitud);
        }

        [HttpGet]
        [Route("/api/SegundoParcial/ObtenerCategorias")]
        public IActionResult ObtenerCategorias()
        {
            var resultado = ObtenerResultado(_service.ObtenerCategorias());
            return Ok(resultado);
        }

        [HttpGet]
        [Route("/api/SegundoParcial/ObtenerProductosPorIdProveedor/{idProveedor}")]
        public IActionResult ObtenerProductosPorIdProveedor(int idProveedor)
        {
            var resultado = ObtenerResultado(_service.ObtenerProductosPorIdProveedor(idProveedor));
            return Ok(resultado);
        }

        [HttpGet]
        [Route("/api/SegundoParcial/ObtenerClientesPorIdProductoVenta/{idProducto}")]
        public IActionResult ObtenerClientesPorIdProductoVenta(int idProducto)
        {
            var resultado = ObtenerResultado(_service.ObtenerClientesPorIdProductoVenta(idProducto));
            return Ok(resultado);
        }

        private object ObtenerResultado(XmlElement xmlElement)
        {
            XElement rootElement = XElement.Parse(xmlElement.OuterXml);
            rootElement.RemoveAttributes();
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(rootElement.ToString());
            return JsonConvert.SerializeXmlNode(xmlDocument);
        }
    }
}