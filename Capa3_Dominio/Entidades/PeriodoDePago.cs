using System;
using System.Collections.Generic;
using System.Text;

namespace Capa3_Dominio.Entidades
{
    public class PeriodoDePago
    {
        private int idPeriodo;
        private Boolean estado; //Periodo activo (1) y procesado(0)
        private DateTime fechaFin;
        private DateTime fechaInicio;
        
        public int IdPeriodo { get => idPeriodo; set => idPeriodo = value; }
        public bool Estado { get => estado; set => estado = value; }
        public DateTime FechaFin { get => fechaFin; set => fechaFin = value; }
        public DateTime FechaInicio { get => fechaInicio; set => fechaInicio = value; }

        public int calcularCantidadDeSemanas()
        {
            TimeSpan resultado = fechaFin - fechaInicio;
            int cantidadDias = resultado.Days;
            Console.WriteLine(resultado);
            Console.WriteLine(resultado.Days);
            int cantidadSemanas = cantidadDias / 7;
            return cantidadSemanas;
        }

        public bool ValidarFechaDePagoActivo()
        {
            DateTime FechaActual = DateTime.Now;
            int res = DateTime.Compare(FechaActual.Date, fechaFin.Date);
            if (res <= 0)
            {
                return false;
            }
            return true;
        }
    }
}
