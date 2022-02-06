using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa3_Dominio.Contratos;
using Capa3_Dominio.Entidades;
using Capa4_Persistencia.FabricaDatos;

namespace Capa2_Aplicacion.Servicios
{
    public class GestionarPagoServicio
    {
        private IGestorAccesoDatos gestorAccesoDatos;
        private IEmpleado empleadoDAO;
        private IContrato contratoDAO;
        private ISeguro seguroDAO;
        private IPago pagoDAO;
        private IConceptos conceptosDAO;
        private IPeriodoDePago periodoDePagoDAO;
        public GestionarPagoServicio()
        {
            FabricaAbstracta fabricaAbstracta = FabricaAbstracta.CrearInstancia();
            gestorAccesoDatos = fabricaAbstracta.CrearGestorAccesoDatos();
            empleadoDAO = fabricaAbstracta.CrearEmpleadoDAO(gestorAccesoDatos);
            contratoDAO = fabricaAbstracta.CrearContratoDAO(gestorAccesoDatos);
            seguroDAO = fabricaAbstracta.CrearSeguroDAO(gestorAccesoDatos);
            conceptosDAO = fabricaAbstracta.CrearConceptosDAO(gestorAccesoDatos);
            pagoDAO = fabricaAbstracta.CrearPagoDAO(gestorAccesoDatos);
            periodoDePagoDAO = fabricaAbstracta.CrearPeriodoDePagoDAO(gestorAccesoDatos);
        }

        public bool pagarContratos(Pago pago)
        {
            bool exitoPago = false;  
            gestorAccesoDatos.AbrirConexion();
            exitoPago = pagoDAO.realizarPagos(pago);
            gestorAccesoDatos.CerrarConexion();
            return exitoPago;
        }

        public ConceptoDeIngresoYDescuento obtenerConcepto(int idPeriodo,int idContrato) {
            ConceptoDeIngresoYDescuento concepto = new ConceptoDeIngresoYDescuento();
            gestorAccesoDatos.AbrirConexion();
            concepto = conceptosDAO.listarConceptosYContratosPorIdPeriodoPago(idPeriodo, idContrato);
            gestorAccesoDatos.CerrarConexion();
            return concepto; 
        }
        public List<Pago> listarPagosRealizados(int idPeriodo) {
            List<Pago> pagosListados = new List<Pago>();
            gestorAccesoDatos.AbrirConexion();
            pagosListados = pagoDAO.listarPagosRealizadoPorPeriodo(idPeriodo);
            gestorAccesoDatos.CerrarConexion();
            return pagosListados; 
        }
        public bool cambiarProcesado(int idPeriodo) {
            bool exito = false;
            gestorAccesoDatos.AbrirConexion();
            exito = periodoDePagoDAO.cambiarProcesado(idPeriodo);
            gestorAccesoDatos.CerrarConexion();
            return exito;
        }
        public PeriodoDePago obtenerPeriodosDePago()
        {
            gestorAccesoDatos.AbrirConexion();
            PeriodoDePago periodo_activo = new PeriodoDePago();
            List<PeriodoDePago> listaPeriodosDePago = new List<PeriodoDePago>();
            listaPeriodosDePago = periodoDePagoDAO.obtenerPeriodosDePago();
            if (listaPeriodosDePago.Count() != 0)
            {
                foreach (PeriodoDePago periodoDePago in listaPeriodosDePago)
                {

                    if (periodoDePago.Estado == true)
                    {
                        periodo_activo = periodoDePago;
                    }
                    else
                    {
                        periodo_activo = null;
                    }
                }
            }
            else { periodo_activo = null; }
            gestorAccesoDatos.CerrarConexion();
            return periodo_activo;
        }
    }
}
