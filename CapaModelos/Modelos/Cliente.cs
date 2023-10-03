namespace CapaModelos.Modelos
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }

        public Cliente()
        {
            IdCliente = 0;
            Nombre = string.Empty;
            Direccion = string.Empty;
            Telefono = string.Empty;
        }
    }
}
