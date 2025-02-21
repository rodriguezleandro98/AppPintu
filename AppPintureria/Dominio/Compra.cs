using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Compra
    {
        public long Id { get; set; }
        public long Recibo { get; set; }
        public Proveedor Proveedor { get; set; }
        public DateTime FechaCompra { get; set; }
        public DateTime FechaEntrega { get; set; }
        public decimal PrecioTotal { get; set; }
        public bool Estado { get; set; }
    }
}
