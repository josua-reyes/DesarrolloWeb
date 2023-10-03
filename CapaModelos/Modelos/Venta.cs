namespace CapaModelos.Modelos
{
    public class Venta
    {
        public int IdVenta { get; set; }
        public int IdFactura { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }

        public Venta()
        {
            IdVenta = 0;
            IdFactura = 0;
            IdProducto = 0;
            Cantidad = 0;
        }
    }
}