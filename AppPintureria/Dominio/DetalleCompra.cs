using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class DetalleCompra
    {
        public long Id { get; set; }
        public Compra Compra { get; set; }
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }
        public int CantidadVieja { get; set; }
        public decimal PrecioCompraUnitario { get; set; }
        public decimal Subtotal { get; set; }
    }
}
