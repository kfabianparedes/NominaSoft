using Capa3_Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa3_Dominio.Contratos
{
    public interface ISeguro
    {
        public Seguro mostrarSeguro(int idSeguro);
        public List<Seguro> obtenerSeguros();
    }
}
