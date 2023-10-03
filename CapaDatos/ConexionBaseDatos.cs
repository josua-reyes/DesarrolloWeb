using Oracle.ManagedDataAccess.Client;
using System.Configuration;

namespace CapaDatos
{
    public class ConexionBaseDatos 
    {     
        internal string _CadenaConexion { get; set; }
        internal OracleConnection _Connection { get; set; }
        internal OracleTransaction _Transaction {  get; set; }
        internal OracleCommand _Command { get; set; }
        public ConexionBaseDatos() 
        {
            _CadenaConexion = ConfigurationManager.AppSettings["CadenaConexionOracle"];
        }
    }
}
