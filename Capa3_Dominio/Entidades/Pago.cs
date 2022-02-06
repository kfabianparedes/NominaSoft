using System;
using System.Collections.Generic;
using System.Text;

namespace Capa3_Dominio.Entidades
{
    public class Pago
    {
        private int idPago;
        private double porcentajeDescuentoAfp; //según el caso de uso 
        private double montoAsignacionFamiliar;
        private int totalDeHoras;    
        private int valorPorHora;
        private DateTime fechaDePago;


        private PeriodoDePago periodoDePago ;
        private Contrato contrato ;
        private ConceptoDeIngresoYDescuento conceptoDeIngresoYDescuento;

        public Pago(PeriodoDePago periodoDePago, Contrato contrato, ConceptoDeIngresoYDescuento conceptoDeIngresoYDescuento)
        {
            this.periodoDePago = periodoDePago;
            this.contrato = contrato;
            this.conceptoDeIngresoYDescuento = conceptoDeIngresoYDescuento;
        }

        public Pago(PeriodoDePago periodoDePago, Contrato contrato)
        {
            this.periodoDePago = periodoDePago;
            this.contrato = contrato;
        }
        public Pago()
        {
            
        }

        public int IdPago { get => idPago; set => idPago = value; }
        public double PorcentajeDescuentoAfp { get => porcentajeDescuentoAfp; set => porcentajeDescuentoAfp = value; }
        public int ValorPorHora { get => valorPorHora; set => valorPorHora = value; }
        public DateTime FechaDePago { get => fechaDePago; set => fechaDePago = value; }
        public Contrato Contrato { get => contrato; set => contrato = value; }
        public PeriodoDePago PeriodoDePago { get => periodoDePago; set => periodoDePago = value; }
        public ConceptoDeIngresoYDescuento ConceptoDeIngresoYDescuento { get => conceptoDeIngresoYDescuento; set => conceptoDeIngresoYDescuento = value; }
        public double MontoAsignacionFamiliar { get => montoAsignacionFamiliar; set => montoAsignacionFamiliar = value; }
        public int TotalDeHoras { get => totalDeHoras; set => totalDeHoras = value; }

        public int calcularTotalDeHoras()
        {
            TotalDeHoras = contrato.TotalHorasPorSemana * periodoDePago.calcularCantidadDeSemanas();
            return TotalDeHoras;
        }
        public int calcularSueldoBasico()
        {
            int sueldoBasico = totalDeHoras * valorPorHora ;
            return sueldoBasico;
        }
        public double calcularTotalIngresos()
        {
            MontoAsignacionFamiliar = contrato.calcularMontoAsignacionFamiliar();
            double totalIngresos = calcularSueldoBasico() + MontoAsignacionFamiliar + conceptoDeIngresoYDescuento.calcularTotalConceptoIngresos();
            return totalIngresos;
        }

        public double calcularTotalDescuentos()
        {
            double totalDescuentos = calcularDescuentoAFP() + conceptoDeIngresoYDescuento.calcularTotalConceptoDescuentos();
            return totalDescuentos;
        }
        public double calcularSueldoNeto()
        {
            double sueldoNeto = calcularTotalIngresos() - calcularTotalDescuentos();
            return sueldoNeto;
        }


        public double calcularDescuentoAFP()
        {
            //descuentoAfp = sueldoBasico * contrato.Seguro.PorcentajeDescuento;
            //return descuentoAfp;
            double descuentoAFP = calcularSueldoBasico() * porcentajeDescuentoAfp/100;
            return descuentoAFP;

        }


    }
}
