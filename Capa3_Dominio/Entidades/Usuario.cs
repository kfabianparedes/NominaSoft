using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capa1_Presentacion
{
    public class Usuario
    {
        private String nombres;
        private String apellidos;

        public Usuario(string nombres, string apellidos)
        {
            this.nombres = nombres;
            this.apellidos = apellidos;
        }

        public string Nombres { get => nombres; set => nombres = value; }
        public string Apellidos { get => apellidos; set => apellidos = value; }
    }
}
