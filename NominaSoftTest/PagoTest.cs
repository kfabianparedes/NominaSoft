
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capa3_Dominio.Entidades;
using System;

namespace ProyectoCalidadTest
{
    [TestClass]
    public class PagoTest
    {
        [TestMethod]
        public void Test1_calcularDescuentosAFP()
        {
            Contrato contrato = new Contrato();
            contrato.TotalHorasPorSemana = 38;
            PeriodoDePago periodoDePago = new PeriodoDePago();
            periodoDePago.FechaInicio = DateTime.Now;
            periodoDePago.FechaFin = DateTime.Now.AddDays(21);
            Pago pago = new Pago(periodoDePago, contrato); 
            pago.ValorPorHora = 8;
            pago.PorcentajeDescuentoAfp = 5;
            double descuentosAFP_esperado = 45.6;
            double descuentosAFP_obtenido = pago.calcularDescuentoAFP();
            Assert.AreEqual(descuentosAFP_esperado, descuentosAFP_obtenido);
        }
        [TestMethod]
        public void Test1_calcularSueldoBasico()
        {
            Contrato contrato = new Contrato();
            PeriodoDePago periodoDePago = new PeriodoDePago();
            contrato.TotalHorasPorSemana = 38;
            periodoDePago.FechaInicio = DateTime.Now;
            periodoDePago.FechaFin = DateTime.Now.AddDays(21);
            Pago pago = new Pago(periodoDePago, contrato);
            pago.ValorPorHora = 8;
            double calcularSueldoBasico_esperado = 912;
            double calcularSueldoBasico_obtenido = pago.calcularSueldoBasico();
            Assert.AreEqual(calcularSueldoBasico_esperado, calcularSueldoBasico_obtenido);
        }


        [TestMethod]
        public void Test1_calcularSueldoNeto()
        {
            Contrato contrato = new Contrato();
            PeriodoDePago periodoDePago = new PeriodoDePago();
            ConceptoDeIngresoYDescuento conceptoDeIngresoYDescuento = new ConceptoDeIngresoYDescuento();
            contrato.TotalHorasPorSemana = 38;
            periodoDePago.FechaInicio = DateTime.Now;
            periodoDePago.FechaFin = DateTime.Now.AddDays(21);
            conceptoDeIngresoYDescuento.MontoPorHorasExtra = 15;
            conceptoDeIngresoYDescuento.MontoReintegros = 15;
            conceptoDeIngresoYDescuento.MontoPorOtrosIngresos = 15;
            conceptoDeIngresoYDescuento.MontoPorHorasAusentes = 50;
            conceptoDeIngresoYDescuento.MontoPorAdelantos = 50;
            conceptoDeIngresoYDescuento.MontoPorOtrosDescuentos = 50;
            contrato.TieneAsignacionFamiliar = true;//93
            Pago pago = new Pago(periodoDePago,contrato,conceptoDeIngresoYDescuento);
            pago.ValorPorHora = 8;
            pago.PorcentajeDescuentoAfp = 5;
            double calcularSueldoNeto_esperado = 854.4;
            double calcularSueldoNeto_obtenido = pago.calcularSueldoNeto();
            Assert.AreEqual(calcularSueldoNeto_esperado, calcularSueldoNeto_obtenido);
        }

        [TestMethod]
        public void Test1_calcularTotalDescuentos()
        {
            Contrato contrato = new Contrato();
            PeriodoDePago periodoDePago = new PeriodoDePago();
            ConceptoDeIngresoYDescuento conceptoDeIngresoYDescuento = new ConceptoDeIngresoYDescuento();
            contrato.TotalHorasPorSemana = 38;
            contrato.TieneAsignacionFamiliar = false;
            periodoDePago.FechaInicio = DateTime.Now;
            periodoDePago.FechaFin = DateTime.Now.AddDays(21);
            conceptoDeIngresoYDescuento.MontoPorHorasAusentes = 50;
            conceptoDeIngresoYDescuento.MontoPorAdelantos = 50;
            conceptoDeIngresoYDescuento.MontoPorOtrosDescuentos = 50;
            Pago pago = new Pago(periodoDePago,contrato,conceptoDeIngresoYDescuento);
            pago.ValorPorHora = 8;
            pago.PorcentajeDescuentoAfp = 5;
            double totalDescuentos_esperado = 195.6;
            double totalDescuentos_obtenido = pago.calcularTotalDescuentos();
            Assert.AreEqual(totalDescuentos_esperado, totalDescuentos_obtenido);
        }
        [TestMethod]
        public void Test1_calcularTotalDeHoras()
        {
            Contrato contrato = new Contrato();
            PeriodoDePago periodoDePago = new PeriodoDePago();
            contrato.TotalHorasPorSemana = 38;
            periodoDePago.FechaInicio = DateTime.Now;
            periodoDePago.FechaFin = DateTime.Now.AddDays(14);
            Pago pago = new Pago(periodoDePago, contrato);
            double calcularTotalDeHoras_esperado = 76;
            double calcularTotalDeHoras_obtenido = pago.calcularTotalDeHoras();
            Assert.AreEqual(calcularTotalDeHoras_esperado, calcularTotalDeHoras_obtenido);
        }


        [TestMethod]
        public void Test1_calcularTotalIngresos()
        {
            Contrato contrato = new Contrato();
            PeriodoDePago periodoDePago = new PeriodoDePago();
            ConceptoDeIngresoYDescuento conceptoDeIngresoYDescuento = new ConceptoDeIngresoYDescuento();
            contrato.TotalHorasPorSemana = 38;
            contrato.TieneAsignacionFamiliar = true;
            periodoDePago.FechaInicio = DateTime.Now;
            periodoDePago.FechaFin = DateTime.Now.AddDays(21);
            conceptoDeIngresoYDescuento.MontoPorHorasExtra = 15;
            conceptoDeIngresoYDescuento.MontoReintegros = 15;
            conceptoDeIngresoYDescuento.MontoPorOtrosIngresos = 15;
            Pago pago = new Pago(periodoDePago,contrato,conceptoDeIngresoYDescuento);
            //fechaFin - fechaInicio
            pago.ValorPorHora = 8;
            double calcularTotalIngreso_esperado = 1050;
            double calcularTotalIngreso_obtenido = pago.calcularTotalIngresos();
            Assert.AreEqual(calcularTotalIngreso_esperado, calcularTotalIngreso_obtenido);
        }
    }
}
