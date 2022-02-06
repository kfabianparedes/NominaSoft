using System;
using System.Collections.Generic;
using System.Text;

namespace Capa3_Dominio.Entidades
{
    public class Contrato
    {
        private int idContrato;
        private bool esVigente;
        private bool estaAnulado;
        private DateTime fechaInicial;
        private DateTime fechaFinal;
        private bool tieneAsignacionFamiliar;
        private int totalHorasPorSemana;
        private int valorPorHora;
        private string cargo; 

        private Seguro seguro;
        private Empleado empleado;

        public Contrato(bool esVigente, bool estaAnulado, DateTime fechaInicial, DateTime fechaFinal,
            bool tieneAsignacionFamiliar, int totalHorasPorSemana, int valorPorHora, string cargo,
            Seguro seguro, Empleado empleado, ConceptoDeIngresoYDescuento conceptoDeIngresoYDescuento)
        {
            this.esVigente = esVigente;
            this.estaAnulado = estaAnulado;
            this.fechaInicial = fechaInicial;
            this.fechaFinal = fechaFinal;
            this.tieneAsignacionFamiliar = tieneAsignacionFamiliar;
            this.totalHorasPorSemana = totalHorasPorSemana;
            this.valorPorHora = valorPorHora;
            this.cargo = cargo;
            this.seguro = seguro;
            this.empleado = empleado;
            this.conceptoDeIngresoYDescuento = conceptoDeIngresoYDescuento;
        }

        public Contrato()
        {
        }

        public Contrato(bool esVigente, bool estaAnulado, DateTime fechaInicial, DateTime fechaFinal, bool tieneAsignacionFamiliar,
            int totalHorasPorSemana, int valorPorHora, string cargo)
        {
            this.esVigente = esVigente;
            this.estaAnulado = estaAnulado;
            this.fechaInicial = fechaInicial;
            this.fechaFinal = fechaFinal;
            this.tieneAsignacionFamiliar = tieneAsignacionFamiliar;
            this.totalHorasPorSemana = totalHorasPorSemana;
            this.valorPorHora = valorPorHora;
            this.cargo = cargo;
        }

        private ConceptoDeIngresoYDescuento conceptoDeIngresoYDescuento = new ConceptoDeIngresoYDescuento();
        public int IdContrato { get => idContrato; set => idContrato = value; }
        public bool EsVigente { get => esVigente; set => esVigente = value; }
        public DateTime FechaInicial { get => fechaInicial; set => fechaInicial = value; }
        public DateTime FechaFinal { get => fechaFinal; set => fechaFinal = value; }
        public bool TieneAsignacionFamiliar { get => tieneAsignacionFamiliar; set => tieneAsignacionFamiliar = value; }
        public int TotalHorasPorSemana { get => totalHorasPorSemana; set => totalHorasPorSemana = value; }
        public int ValorPorHora { get => valorPorHora; set => valorPorHora = value; }
        public bool EstaAnulado { get => estaAnulado; set => estaAnulado = value; }
        public ConceptoDeIngresoYDescuento ConceptoDeIngresoYDescuento { get => conceptoDeIngresoYDescuento; set => conceptoDeIngresoYDescuento = value; }
        public Seguro Seguro { get => seguro; set => seguro = value; }
        public Empleado Empleado { get => empleado; set => empleado = value; }
        public string Cargo { get => cargo; set => cargo = value; }

        public Double calcularMontoAsignacionFamiliar() //Para realizar el pago verificamos si cuenta con asignacion familiar
        {
            if (tieneAsignacionFamiliar)
            {
                return 930 * 0.1;
            }
            return 0.0;
        }
        public Boolean verificarEsVigente() //Vericiar si el contrato es vigente para realizar el pago

        {
            EsVigente = false;
            DateTime FechaActual = DateTime.Now;
            int res = DateTime.Compare(fechaFinal, FechaActual);
            if (res >= 0 && estaAnulado == false)
            {
                EsVigente = true;
            }
            return EsVigente;
        }
        // Nos sirve para verificar que cuando vamos a crear un contrato (No menos a 3 meses)
        public Boolean verificarFechaFin() 
        {
            TimeSpan resultado = fechaFinal - fechaInicial;
            int dias = resultado.Days;
            bool verificacion = false;

            if (dias >= 90 && dias <= 365)
            {
                verificacion = true;
            }
            return verificacion;

        }
        public bool VerificarTotalHorasContratadasPorSemana() // Cantidad de horas por semana de un contrato
        {
            bool verficacion = false;
            if (totalHorasPorSemana >= 8 && totalHorasPorSemana <= 40 && totalHorasPorSemana % 4 == 0)
            {
                verficacion= true;
            }
            return verficacion;

        }
        public bool verificarValorPorHora()
        {
            if (valorPorHora >= 10 && valorPorHora <= 60)
                return true;
            else return false;
        }

    }
}
