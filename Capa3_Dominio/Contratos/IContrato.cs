using Capa3_Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa3_Dominio.Contratos
{
    public interface IContrato
    {
        public int crearContrato(Contrato contrato);

        public int editarContrato(Contrato contrato);
        public List<Contrato> buscarContratoPorIdEmpleado(int idEmpleado);

        public List<Contrato> listarContratosPorPeriodo(int idPeriodo);
        public int anularContrato(int idContrato);

    }
}
