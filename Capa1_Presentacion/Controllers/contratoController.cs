using Capa3_Dominio.Entidades;
using Capa1_Presentacion.Filters;
using Capa2_Aplicacion.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capa1_Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class contratoController : ControllerBase
    {
        [HttpPost]
        public ActionResult<int> crearContrato(Contrato contrato)
        {
            GestionarContratoServicio gestionarContratoServicio = new GestionarContratoServicio();
            Mensaje_Error mensaje_error = new Mensaje_Error();
            if (gestionarContratoServicio.VerificarContratoAnterior(contrato.Empleado.IdEmpleado, contrato)==false)
            {
                mensaje_error = new Mensaje_Error("No se puede registrar el contrato”. La fecha de inicio es menor a la fecha fin del contrato anterior.");
                return BadRequest(mensaje_error);
            }
            
            if (contrato.FechaInicial.Date.ToString() == null)
            {
                mensaje_error = new Mensaje_Error("La fecha de inicio esta vacia");
                return BadRequest(mensaje_error);
            }
            //Fecha Fin
            if (contrato.FechaFinal.Date.ToString() == null)
            {
                mensaje_error = new Mensaje_Error("La fecha de fin esta vacia");
                return BadRequest(mensaje_error);
            }
            //Cargo
            if (contrato.Cargo == null)
            {
                mensaje_error = new Mensaje_Error("El cargo esta vacio");
                return BadRequest(mensaje_error);
            }
            //AFP
            if (contrato.Seguro.IdSeguro == 0)
            {
                mensaje_error = new Mensaje_Error("El seguro AFP esta vacio");
                return BadRequest(mensaje_error);
            }
            //Asignacion familiar
            if (contrato.TieneAsignacionFamiliar != true && contrato.TieneAsignacionFamiliar != false)
            {
                mensaje_error = new Mensaje_Error("La asignacion familiar esta vacio");
                return BadRequest(mensaje_error);
            }
            //Total horas contratadas por semana
            if (contrato.TotalHorasPorSemana == 0)
            {
                mensaje_error = new Mensaje_Error("El total horas por semana esta vacio");
                return BadRequest(mensaje_error);
            }
            //Valor por hora
            if (contrato.ValorPorHora == 0)
            {
                mensaje_error = new Mensaje_Error("El valor por hora esta vacio");
                return BadRequest(mensaje_error);
            }
            if (contrato.verificarFechaFin() == false)
            {
                mensaje_error = new Mensaje_Error("La fecha fin de un contrato debe ser superior a la fecha de inicio con una diferencia de tres meses como mínimo");
                return BadRequest(mensaje_error);
            }
            if (contrato.VerificarTotalHorasContratadasPorSemana() == false)
            {
                mensaje_error = new Mensaje_Error("El total de horas contratadas por semana sólo puede ser de 8 a 40 horas con diferencia de 4 horas a partir de 8 horas");
                return BadRequest(mensaje_error);
            }
            if (contrato.verificarValorPorHora() == false)
            {
                mensaje_error = new Mensaje_Error("El valor hora de un contrato sólo puede ser desde 10 soles a 60 soles, pero sólo valores enteros.");
                return BadRequest(mensaje_error);
            }
            return Ok(gestionarContratoServicio.CrearContrato(contrato));
        }
        [HttpGet("{idEmpleado}")]
        public ActionResult<Contrato> retornarContrato(int idEmpleado)
        {
            Contrato contrato = new Contrato();
            Mensaje_Error mensaje_error = new Mensaje_Error();
            GestionarContratoServicio gestionarContratoServicio = new GestionarContratoServicio();
            contrato = gestionarContratoServicio.buscarContratoPorEmpleado(idEmpleado);
            if (contrato == null)
            {
                mensaje_error = new Mensaje_Error("No existe un contrato vigente");
                return BadRequest(mensaje_error);
            }
            return Ok(contrato);
        }
        [HttpGet("anular-contrato/{idContrato}")]
        public ActionResult<int> anularContrato(int idContrato)
        {
            int anular;
            Mensaje_Error mensaje_error = new Mensaje_Error();
            GestionarContratoServicio gestionarContratoServicio = new GestionarContratoServicio();
            anular =  gestionarContratoServicio.anularContrato(idContrato);
            if (idContrato.ToString() == "")
            {
                mensaje_error = new Mensaje_Error("Tiene que mandar la variable idContrato");
                return BadRequest(mensaje_error);
            }
            if (anular == 0) {
                mensaje_error = new Mensaje_Error("El idContrato enviado no existe");
                return BadRequest(mensaje_error);
            }
            if(anular == 1) {
                return Ok(anular);
            }
            mensaje_error = new Mensaje_Error("error, Intente de nuevo");
            return BadRequest(mensaje_error);
        }
        [HttpGet("listarContratos/{idPeriodo}")]
        public ActionResult<List<Contrato>> retornarListaContratos(int idPeriodo)
        {
            List<Contrato> listaContratos = new List<Contrato>();
            Mensaje_Error mensaje_error = new Mensaje_Error();
            List<Contrato> listaContratosValidos = new List<Contrato>();
            GestionarContratoServicio gestionarContratoServicio = new GestionarContratoServicio();
            listaContratos = gestionarContratoServicio.listarContratosPorPeriodo(idPeriodo);
            if (listaContratos == null)
            {
                mensaje_error = new Mensaje_Error("No se puede procesar porque no existen contratos");
                return BadRequest(mensaje_error);
            }
            else
            {
                foreach (Contrato cont in listaContratos) {
                    if (cont.verificarValorPorHora()) {
                        if (cont.VerificarTotalHorasContratadasPorSemana())
                        {
                            if (cont.verificarFechaFin())
                            {
                                if (cont.verificarEsVigente())
                                {
                                    listaContratosValidos.Add(cont);
                                }
                            }
                        }  
                    }
                }
                return Ok(listaContratosValidos);
            }

        }
        [HttpPut]
        public ActionResult<int> ActualizarContratoPorId(Contrato contrato)
        {
           
            int actualizar=0;
            Mensaje_Error mensaje_error = new Mensaje_Error();
            GestionarContratoServicio gestionarContratoServicio = new GestionarContratoServicio();
            if (gestionarContratoServicio.VerificarContratoAnterior(contrato.Empleado.IdEmpleado,contrato))
            {
                mensaje_error = new Mensaje_Error("No se guardó las modificaciones del contrato”. La nueva fecha de inicio del contrato actual es menor a la fecha fin del contrato anterior.");
                return BadRequest(mensaje_error);
            }
            if (contrato.verificarFechaFin()==false)
            {
                mensaje_error = new Mensaje_Error("No se guardó las modificaciones del contrato”. El contrato debe durar como mínimo 3 meses.");
                return BadRequest(mensaje_error);
            }
            
            if (contrato.VerificarTotalHorasContratadasPorSemana() == false)
            {
                mensaje_error = new Mensaje_Error("No se guardó las modificaciones del contrato”. El total de horas por semana debe ser múltiplo de 4 que estan entre 8 y 40");
                return BadRequest(mensaje_error);
            }
               
             if (contrato.verificarValorPorHora() == false)
             {
                  mensaje_error = new Mensaje_Error("No se guardó las modificaciones del contrato”. El valor por hora no debe ser menor a 10 ni mayor a 60.");
                  return BadRequest(mensaje_error);
             }
            actualizar = gestionarContratoServicio.actualizarContrato(contrato);
            return Ok(actualizar);

        }
        
        
        [HttpGet("verificar-vigente/{idEmpleado}")]
        public ActionResult<bool> verificarVigenciaAnteriorContrato(int idEmpleado)
        {
            bool respuesta=false;
            GestionarContratoServicio gestionarContratoServicio = new GestionarContratoServicio();
            Mensaje_Error mensaje_error = new Mensaje_Error();
            if (gestionarContratoServicio.VerificarEsVigenteContratoAnterior(idEmpleado) == true)
            {
                mensaje_error = new Mensaje_Error("Ya existe un contrato vigente.");
                return BadRequest(mensaje_error);
            }
            return Ok(respuesta);
        }
    }
}
