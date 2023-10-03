namespace CapaModelos.DTO
{
    public class GuardarFacturaVentaRespuesta : CamposGenericosRespuesta
    {
        public int NumeroFactura { get; set; }
        public int CantidadDetalleVenta { get; set; }

        public GuardarFacturaVentaRespuesta()
        {
            NumeroFactura = 0;
            CantidadDetalleVenta = 0;
        }
    }
}
