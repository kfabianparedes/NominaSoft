using Capa3_Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa3_Dominio.Contratos
{
    public interface IPeriodoDePago
    {
        public List<PeriodoDePago> obtenerPeriodosDePago();

        public bool cambiarProcesado(int idPeriodo);

    }
}
