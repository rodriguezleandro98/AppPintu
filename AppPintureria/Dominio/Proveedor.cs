using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Proveedor
    {
        public int Id { get; set; }
        public long CUIT { get; set; }
        public string Siglas { get; set; }
        public Contacto Contacto { get; set; }
        public bool Activo { get; set; }
    }
}
