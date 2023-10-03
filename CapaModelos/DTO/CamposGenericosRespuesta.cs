namespace CapaModelos
{
    public class CamposGenericosRespuesta
    {
        private string descripcionError;

        public string Estado { get; set; }
        public string DescripcionError
        {
            get
            {
                return descripcionError;
            }
            set
            {
                descripcionError = (value != "null" && value != null) ? value : string.Empty;
            }
        }


        public CamposGenericosRespuesta()
        {
            Estado = string.Empty;
            DescripcionError = string.Empty;
        }
    }
}
