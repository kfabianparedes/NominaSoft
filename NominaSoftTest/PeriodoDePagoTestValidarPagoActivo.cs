using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capa3_Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoCalidadTest
{
    [TestClass]
    public class PeriodoDePagoTestValidarPagoActivo
    {
        [TestMethod]
        public void test1_calcularCantidadDeSemanas()
        {
            PeriodoDePago periodoDePago = new PeriodoDePago();
            periodoDePago.FechaInicio = DateTime.Now;
            periodoDePago.FechaFin = DateTime.Now.AddDays(186);
            int cantidadDeSemanas_esperado = 26;
            int cantidadDeSemanas_obtenido = periodoDePago.calcularCantidadDeSemanas();
            Assert.AreEqual(cantidadDeSemanas_esperado, cantidadDeSemanas_obtenido);
        }

   

        [TestMethod]
        public void Test_1ValidarFechaDePagoActivo()
        {
            PeriodoDePago periodoDePago = new PeriodoDePago();
            periodoDePago.FechaFin = DateTime.Now.AddDays(1);
            bool validarFechaDePagoActivo_esperado = true;
            bool validarFechaDePagoActivo_obtenido = periodoDePago.ValidarFechaDePagoActivo();
            Assert.AreEqual(validarFechaDePagoActivo_esperado, validarFechaDePagoActivo_obtenido);
        }
        [TestMethod]
        public void Test_2ValidarFechaDePagoActivo()
        {
            PeriodoDePago periodoDePago = new PeriodoDePago();
            periodoDePago.FechaFin = DateTime.Now;
            bool validarFechaDePagoActivo_esperado = true;
            bool validarFechaDePagoActivo_obtenido = periodoDePago.ValidarFechaDePagoActivo();
            Assert.AreEqual(validarFechaDePagoActivo_esperado, validarFechaDePagoActivo_obtenido);
        }
        [TestMethod]
        public void Test_3ValidarFechaDePagoActivo()
        {
            PeriodoDePago periodoDePago = new PeriodoDePago();
            periodoDePago.FechaFin = DateTime.Now.AddDays(-1);
            bool validarFechaDePagoActivo_esperado = false;
            bool validarFechaDePagoActivo_obtenido = periodoDePago.ValidarFechaDePagoActivo();
            Assert.AreEqual(validarFechaDePagoActivo_esperado, validarFechaDePagoActivo_obtenido);
        }
    }
}
