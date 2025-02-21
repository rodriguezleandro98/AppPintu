using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Venta
    {
        public long Id { get; set; }
        public decimal PrecioTotal { get; set; }
        public DateTime FechaVenta { get; set; }
        public int NroFactura { get; set; }
    }
}
