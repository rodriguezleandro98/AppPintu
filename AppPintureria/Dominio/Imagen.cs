using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Imagen
    {
        public long ID { get; set; }
        public string ImagenUrl { get; set; }
        public bool Activo { get; set; }
    }
}
