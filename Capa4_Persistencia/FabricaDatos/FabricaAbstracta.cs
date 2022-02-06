using Capa3_Dominio.Contratos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capa4_Persistencia.FabricaDatos
{
    public abstract class FabricaAbstracta
    {
        public static FabricaAbstracta CrearInstancia()
        {
            return new FabricaMySQL();
        }
        public abstract IGestorAccesoDatos CrearGestorAccesoDatos();
        public abstract IEmpleado CrearEmpleadoDAO(IGestorAccesoDatos gestorAccesoDatos);
        public abstract IContrato CrearContratoDAO(IGestorAccesoDatos gestorAccesoDatos);
        public abstract ISeguro CrearSeguroDAO(IGestorAccesoDatos gestorAccesoDatos);
        public abstract IPeriodoDePago CrearPeriodoDePagoDAO(IGestorAccesoDatos gestorAccesoDatos);
        public abstract IPago CrearPagoDAO(IGestorAccesoDatos gestorAccesoDatos);
        public abstract IConceptos CrearConceptosDAO(IGestorAccesoDatos gestorAccesoDatos);
    }   
}
