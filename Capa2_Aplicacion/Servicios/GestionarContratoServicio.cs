using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa3_Dominio.Contratos;
using Capa3_Dominio.Entidades;
using Capa3_Dominio.Servicios;
using Capa4_Persistencia.FabricaDatos;

namespace Capa2_Aplicacion.Servicios
{
    public class GestionarContratoServicio
    {
        private IGestorAccesoDatos gestorAccesoDatos;
        private IEmpleado empleadoDAO;
        private IContrato contratoDAO;
        private ISeguro seguroDAO;
        private IPago pagoDAO;
        private IConceptos conceptosDAO;
        public GestionarContratoServicio()
        {
            FabricaAbstracta fabricaAbstracta = FabricaAbstracta.CrearInstancia();
            gestorAccesoDatos = fabricaAbstracta.CrearGestorAccesoDatos();
            empleadoDAO = fabricaAbstracta.CrearEmpleadoDAO(gestorAccesoDatos);
            contratoDAO = fabricaAbstracta.CrearContratoDAO(gestorAccesoDatos);
            seguroDAO = fabricaAbstracta.CrearSeguroDAO(gestorAccesoDatos);
        }
        public Empleado buscarEmpleado(int dni)
        {
            gestorAccesoDatos.AbrirConexion();
            Empleado empleado = empleadoDAO.buscarEmpleadoPorDni(dni);
            gestorAccesoDatos.CerrarConexion();
            return empleado;
        }

        public int CrearContrato(Contrato contrato)
        {
            int hecho;
            gestorAccesoDatos.AbrirConexion();
            hecho = contratoDAO.crearContrato(contrato);
            gestorAccesoDatos.CerrarConexion();
            return hecho;
        }

        public Contrato buscarContratoPorEmpleado(int idEmpleado)
        {
            gestorAccesoDatos.AbrirConexion();
            Contrato contrato_enviado = new Contrato();
            List<Contrato> listacontrato = contratoDAO.buscarContratoPorIdEmpleado(idEmpleado);

            foreach (Contrato contrato in listacontrato)
            {
                if (contrato.verificarEsVigente() == true)
                {
                    contrato_enviado = contrato;
                }
                else
                {
                    contrato_enviado = null;
                }
            }
            gestorAccesoDatos.CerrarConexion();
            return contrato_enviado;
        }
        public bool VerificarContratoAnterior(int idEmpleado, Contrato contrato)
        {
            gestorAccesoDatos.AbrirConexion();
            Contrato contrato_actual = contrato;
            List<Contrato> listacontrato = contratoDAO.buscarContratoPorIdEmpleado(idEmpleado);
            VerificarUltimoContratoVigente verificarUltimoContratoVigente = new VerificarUltimoContratoVigente();
            if (verificarUltimoContratoVigente.verificarFechaFinAnteriorContrato(listacontrato, contrato_actual))
            {
                return true;
            }
            gestorAccesoDatos.CerrarConexion();
            return false;
        }
        public bool VerificarEsVigenteContratoAnterior(int idEmpleado)
        {
            gestorAccesoDatos.AbrirConexion();
            List<Contrato> listacontrato = contratoDAO.buscarContratoPorIdEmpleado(idEmpleado);
            VerificarUltimoContratoVigente verificarUltimoContratoVigente = new VerificarUltimoContratoVigente();
            if (verificarUltimoContratoVigente.esVigenteContratoAnterior(listacontrato))
            {
                return true;
            }
            gestorAccesoDatos.CerrarConexion();
            return false;
        }
        public int anularContrato(int idContrato)
        {
            int anulado; 
            gestorAccesoDatos.AbrirConexion();
            anulado = contratoDAO.anularContrato(idContrato);
            gestorAccesoDatos.CerrarConexion();
            return anulado; 
        }
        public List<Contrato> listarContratosPorPeriodo(int idEmpleado){
            gestorAccesoDatos.AbrirConexion();
            List<Contrato> listaContratos= new List<Contrato>();
            listaContratos = contratoDAO.listarContratosPorPeriodo(idEmpleado);
            gestorAccesoDatos.CerrarConexion();
            return listaContratos;
        }
        public int actualizarContrato(Contrato contrato)
        {
            int actualizar = 0;
            gestorAccesoDatos.AbrirConexion();
            actualizar=contratoDAO.editarContrato(contrato);
            gestorAccesoDatos.CerrarConexion();
            return actualizar;

        }

        public List<Seguro> obtenerTodoLosSeguro()
        {
            gestorAccesoDatos.AbrirConexion();
            List<Seguro> listaSeguro = new List<Seguro>();
            listaSeguro = seguroDAO.obtenerSeguros();
            gestorAccesoDatos.CerrarConexion();
            return listaSeguro;
        }
        public Empleado buscarEmpleadoPorId(int idEmpleado)
        {
            gestorAccesoDatos.AbrirConexion();
            Empleado empleado = empleadoDAO.buscarEmpleadoId(idEmpleado);
            gestorAccesoDatos.CerrarConexion();
            return empleado;
        }
        public Seguro buscarSeguroPorId(int idSeguro)
        {
            gestorAccesoDatos.AbrirConexion();
            Seguro seguro = seguroDAO.mostrarSeguro(idSeguro);
            gestorAccesoDatos.CerrarConexion();
            return seguro;
        }
    }
}
