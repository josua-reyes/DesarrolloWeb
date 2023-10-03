using System;
using System.Collections.Generic;

namespace CapaModelos.Modelos
{
    public class Factura
    {
        public int IdFactura { get; set; }
        public DateTime Fecha { get; set; }
        public int IdCliente { get; set; }
        public List<Venta> DetalleVentas { get; set; }

        public Factura()
        {
            IdFactura = 0;
            Fecha = DateTime.MinValue;
            IdCliente = 0;
            DetalleVentas = new List<Venta>();
        }
    }
}
