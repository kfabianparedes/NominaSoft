using Capa2_Aplicacion.Servicios;
using Capa3_Dominio.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capa1_Presentacion.Filters;

namespace Capa1_Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class periodoController : ControllerBase
    {
        [HttpGet]
        public ActionResult<PeriodoDePago> mostrarPeriodoPagoActivo()
        {
            PeriodoDePago periodoDePago = new PeriodoDePago();
            Mensaje_Error mensaje_error = new Mensaje_Error();
            GestionarPagoServicio gestionarPagoServicio = new GestionarPagoServicio();
            periodoDePago = gestionarPagoServicio.obtenerPeriodosDePago();
            if(periodoDePago == null)
            {
                mensaje_error = new Mensaje_Error("No se puede procesar porque no existe un periodo de pago activo.");
                return BadRequest(mensaje_error);
            }
            return Ok(periodoDePago);
        }
        [HttpPut]
        public ActionResult<bool> cambiarProcesado(int idPeriodo)
        {
            bool exito = false;
            GestionarPagoServicio gestionarPagoServicio = new GestionarPagoServicio();
            Mensaje_Error mensaje_error = new Mensaje_Error();
            exito = gestionarPagoServicio.cambiarProcesado(idPeriodo);
            if (!exito)
            {
                mensaje_error = new Mensaje_Error("No se pudo procesar el periodo de pago.");
                return BadRequest(mensaje_error);
            }
            return Ok(exito);
        }
        [HttpPost]
        public ActionResult<bool> verificarProcesarPeriodo(PeriodoDePago periodoDePago)
        {
            PeriodoDePago periodo = new PeriodoDePago();
            periodo = periodoDePago;
            Mensaje_Error mensaje_error = new Mensaje_Error();
            if (periodo == null)
            {
                mensaje_error = new Mensaje_Error("No se puede procesar porque no existe un periodo de pago activo.");
                return BadRequest(mensaje_error);
            }
            if (periodo.ValidarFechaDePagoActivo() != true) {
                mensaje_error = new Mensaje_Error("No se puede procesar porque la fecha actual es menor a la fecha fin del periodo.");
                return BadRequest(mensaje_error);
            }
            return Ok(true);
        }
    }
}
