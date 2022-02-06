using System;
using System.Collections.Generic;
using System.Text;

namespace Capa3_Dominio.Entidades
{
    public class ConceptoDeIngresoYDescuento
    {
        private int idConcepto;
        private float montoPorAdelantos;
        private float montoPorHorasAusentes;
        private float montoPorHorasExtra;
        private float montoPorOtrosDescuentos;
        private float montoPorOtrosIngresos;
        private float montoReintegros;

        private PeriodoDePago periodoDePago;
        private Contrato contrato;

        public int IdConcepto { get => idConcepto; set => idConcepto = value; }
        public float MontoPorAdelantos { get => montoPorAdelantos; set => montoPorAdelantos = value; }
        public float MontoPorHorasAusentes { get => montoPorHorasAusentes; set => montoPorHorasAusentes = value; }
        public float MontoPorHorasExtra { get => montoPorHorasExtra; set => montoPorHorasExtra = value; }
        public float MontoPorOtrosDescuentos { get => montoPorOtrosDescuentos; set => montoPorOtrosDescuentos = value; }
        public float MontoPorOtrosIngresos { get => montoPorOtrosIngresos; set => montoPorOtrosIngresos = value; }
        public float MontoReintegros { get => montoReintegros; set => montoReintegros = value; }
        public PeriodoDePago PeriodoDePago { get => periodoDePago; set => periodoDePago = value; }
        public Contrato Contrato { get => contrato; set => contrato = value; }

        public float calcularTotalConceptoIngresos()
        {
            return montoPorHorasExtra + montoReintegros + montoPorOtrosIngresos;
        }

        public float calcularTotalConceptoDescuentos()
        {
            return montoPorHorasAusentes + montoPorAdelantos + montoPorOtrosDescuentos;
        }
    }
}
