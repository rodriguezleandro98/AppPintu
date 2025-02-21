using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Producto_x_Proveedor
    {
        public long IDProducto { get; set; }
        public int IDProveedor { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal PorcentajeGanancia { get; set; }
    }
}
