namespace CapaModelos.Modelos
{
    public class Proveedor
    {
        public int IdProveedor { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }

        public Proveedor()
        {
            IdProveedor = 0;
            Nombre = string.Empty;
            Direccion = string.Empty;
            Telefono = string.Empty;
        }
    }
}
