using CapaModelos.Modelos;

namespace CapaModelos.DTO
{
    public class GuardarFacturaVentaSolicitud
    {
        public Factura Factura {  get; set; }

        public GuardarFacturaVentaSolicitud()
        {
            Factura = new Factura();
        }
    }
}
