using Capa3_Dominio.Contratos;
using Capa4_Persistencia.ADONet_MySQL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capa4_Persistencia.FabricaDatos
{
    public class FabricaMySQL : FabricaAbstracta
    {
        public override IGestorAccesoDatos CrearGestorAccesoDatos()
        {
            return new GestorMySQL();
        }
        public override IEmpleado CrearEmpleadoDAO(IGestorAccesoDatos gestorAccesoDatos)
        {
            return new EmpleadoMySQL(gestorAccesoDatos);
        }

        public override IContrato CrearContratoDAO(IGestorAccesoDatos gestorAccesoDatos)
        {
            return new ContratoMySQL(gestorAccesoDatos);
        }
        public override ISeguro CrearSeguroDAO(IGestorAccesoDatos gestorAccesoDatos)
        {
            return new SeguroMySQL(gestorAccesoDatos);
        }

        public override IPeriodoDePago CrearPeriodoDePagoDAO(IGestorAccesoDatos gestorAccesoDatos)
        {
            return new PeriodoDePagoMySQL(gestorAccesoDatos);
        }
        public override IPago CrearPagoDAO(IGestorAccesoDatos gestorAccesoDatos)
        {
            return new PagoMySQL(gestorAccesoDatos);
        }

        public override IConceptos CrearConceptosDAO(IGestorAccesoDatos gestorAccesoDatos)
        {
            return new ConceptosMySQL(gestorAccesoDatos);
        }
    }
}
