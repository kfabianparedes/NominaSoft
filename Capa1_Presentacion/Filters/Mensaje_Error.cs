using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capa1_Presentacion.Filters
{
    public class Mensaje_Error
    {
        private String mensaje;

        public Mensaje_Error(string mensaje)
        {
            this.mensaje = mensaje;
        }
        public Mensaje_Error()
        {
        }
        public string Mensaje { get => mensaje; set => mensaje = value; }
    }
}
