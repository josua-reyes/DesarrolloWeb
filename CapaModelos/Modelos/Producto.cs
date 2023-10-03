namespace CapaModelos.Modelos
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int IdCategoria { get; set; }
        public int IdProveedor { get; set; }

        public Producto()
        {
            IdProducto = 0;
            Descripcion = string.Empty;
            Precio = 0;
            IdCategoria = 0;
            IdProveedor = 0;
        }
    }
}
