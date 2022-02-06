using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capa2_Aplicacion.Servicios;
using Capa3_Dominio.Entidades;
using Capa1_Presentacion.Filters;

namespace Capa1_Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class pagoController : ControllerBase
    {
        [HttpPost]
        public ActionResult<List<Pago>> realizarPagos(List<Contrato> listaContratosPagar) {
           
            List<Pago> listaPagos = new List<Pago>();
            List<ConceptoDeIngresoYDescuento> listaConceptos = new List<ConceptoDeIngresoYDescuento>();
            ConceptoDeIngresoYDescuento conceptos = new ConceptoDeIngresoYDescuento();
            GestionarPagoServicio gestionarPagoServicio = new GestionarPagoServicio();
            Mensaje_Error mensaje_error = new Mensaje_Error();
            foreach (Contrato cont in listaContratosPagar) {
               Pago pago = new Pago();
                conceptos = gestionarPagoServicio.obtenerConcepto(cont.ConceptoDeIngresoYDescuento.PeriodoDePago.IdPeriodo,cont.IdContrato);
                conceptos.Contrato = cont;
                pago.ConceptoDeIngresoYDescuento = conceptos;
                pago.Contrato = cont;
                pago.PeriodoDePago = conceptos.PeriodoDePago;
                pago.TotalDeHoras = pago.calcularTotalDeHoras();
                pago.ValorPorHora = pago.Contrato.ValorPorHora;
                pago.MontoAsignacionFamiliar = pago.Contrato.calcularMontoAsignacionFamiliar();
                pago.PorcentajeDescuentoAfp = pago.Contrato.Seguro.PorcentajeDescuento;
                pago.FechaDePago = DateTime.Now;    
                listaPagos.Add(pago);
            }
            foreach (Pago pag in listaPagos) {
                if (!gestionarPagoServicio.pagarContratos(pag)) {

                    mensaje_error = new Mensaje_Error("Hubo un error al realizar el pago de los contratos. Vuelva a intentarlo");
                    return BadRequest(mensaje_error);
                }
            }
            return Ok(listaPagos);
        }

        [HttpPost("calcular")]
        public ActionResult<List<double>> realizarCalculos(Pago pago) {
            List<double> calculos = new List<double>();
            Mensaje_Error mensaje_error = new Mensaje_Error();
            if (pago == null) {
                mensaje_error = new Mensaje_Error("No existe el pago");
                return BadRequest(mensaje_error);
            }
            calculos.Add(pago.calcularSueldoBasico());
            calculos.Add(pago.calcularTotalIngresos());
            calculos.Add(pago.calcularTotalDescuentos());
            calculos.Add(pago.calcularSueldoNeto());


            return Ok(calculos);
        } 
        [HttpGet("{idPeriodo}")]
        public List<Pago> listarPagosRealziados(int idPeriodo) {
            List<Pago> listaPagosRealizados = new List<Pago>();
            GestionarPagoServicio gestionarPagoServicio = new GestionarPagoServicio();

            listaPagosRealizados = gestionarPagoServicio.listarPagosRealizados(idPeriodo);
            return listaPagosRealizados; 
        }
    }
}
