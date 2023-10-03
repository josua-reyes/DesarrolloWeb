namespace CapaModelos.Modelos
{
    public class Categoria
    {
        public int IdCategoria { get; set; }
        public string Descripcion { get; set; }

        public Categoria()
        {
            IdCategoria = 0;
            Descripcion = string.Empty;
        }
    }

}
