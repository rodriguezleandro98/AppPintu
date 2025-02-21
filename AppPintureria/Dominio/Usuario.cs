﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Usuario
    {
        public int Id { get; set; }
        public Permiso Permiso { get; set; }
        public Contacto Contacto { get; set; }
        public Imagen Imagen { get; set; }
        public string NombreUsuario { get; set; }
        public string Contrasenia { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Activo { get; set; }
    }
}
