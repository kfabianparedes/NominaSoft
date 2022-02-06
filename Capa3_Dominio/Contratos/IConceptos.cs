using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa3_Dominio.Entidades;
namespace Capa3_Dominio.Contratos
{
    public interface IConceptos
    {
        ConceptoDeIngresoYDescuento listarConceptosYContratosPorIdPeriodoPago(int idPeriodo,int idContrato);
    }
}
