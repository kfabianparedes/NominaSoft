using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capa3_Dominio.Entidades;
using System;

namespace ProyectoCalidadTest
{

    [TestClass]
    public class ContratoTest
    {

        [TestMethod]
        public void Test_1calcularMontoAsignacionFamiliar()
        {
            Contrato contrato = new Contrato();
            contrato.TieneAsignacionFamiliar = true;
            double verificarMontoAsignacionFamiliar_esperado = 93;
            double verificarMontoAsignacionFamiliar_obtenido = contrato.calcularMontoAsignacionFamiliar();
            Assert.AreEqual(verificarMontoAsignacionFamiliar_esperado, verificarMontoAsignacionFamiliar_obtenido);
        }
        [TestMethod]
        public void Test_2calcularMontoAsignacionFamiliar()
        {
            Contrato contrato = new Contrato();
            contrato.TieneAsignacionFamiliar = false;
            double verificarMontoAsignacionFamiliar_esperado = 0.0;
            double verificarMontoAsignacionFamiliar_obtenido = contrato.calcularMontoAsignacionFamiliar();
            Assert.AreEqual(verificarMontoAsignacionFamiliar_esperado, verificarMontoAsignacionFamiliar_obtenido);
        }
        [TestMethod]
        public void Test_1verificarEsVigente()
        {
            Contrato contrato = new Contrato();
            contrato.FechaFinal = DateTime.Now.AddDays(25);
            contrato.EstaAnulado = false;
            Boolean esVigente_esperado = true;
            Boolean esVigente_obtenido = contrato.verificarEsVigente();
            Assert.AreEqual(esVigente_esperado, esVigente_obtenido);
        }
        [TestMethod]
        public void Test_2verificarEsVigente()
        {
            Contrato contrato = new Contrato();
            contrato.FechaFinal = DateTime.Now.AddDays(-2);
            contrato.EstaAnulado = false;
            Boolean esVigente_esperado = false;
            Boolean esVigente_obtenido = contrato.verificarEsVigente();
            Assert.AreEqual(esVigente_esperado, esVigente_obtenido);
        }
        [TestMethod]
        public void Test_3verificarEsVigente()
        {
            Contrato contrato = new Contrato();
            contrato.FechaFinal = DateTime.Now.AddDays(25);
            contrato.EstaAnulado = true;
            Boolean esVigente_esperado = false;
            Boolean esVigente_obtenido = contrato.verificarEsVigente();
            Assert.AreEqual(esVigente_esperado, esVigente_obtenido);
        }

        [TestMethod]
        public void Test_1VerificarFechaFin()
        {
            Contrato contrato = new Contrato();
            contrato.FechaInicial = DateTime.Now;
            contrato.FechaFinal = DateTime.Now.AddDays(100);
            bool verificarFechaFin_esperado = true;
            bool verificarFechaFin_obtenido = contrato.verificarFechaFin();
            Assert.AreEqual(verificarFechaFin_esperado, verificarFechaFin_obtenido);
        }
        [TestMethod]
        public void Test_2VerificarFechaFin()
        {
            Contrato contrato = new Contrato();
            contrato.FechaInicial = DateTime.Now;
            contrato.FechaFinal = DateTime.Now.AddDays(450);
            bool verificarFechaFin_esperado = false;
            bool verificarFechaFin_obtenido = contrato.verificarFechaFin();
            Assert.AreEqual(verificarFechaFin_esperado, verificarFechaFin_obtenido);
        }
        [TestMethod]
        public void Test_3VerificarFechaFin()
        {
            Contrato contrato = new Contrato();
            contrato.FechaInicial = DateTime.Now;
            contrato.FechaFinal = DateTime.Now.AddDays(60);
            bool verificarFechaFin_esperado = false;
            bool verificarFechaFin_obtenido = contrato.verificarFechaFin();
            Assert.AreEqual(verificarFechaFin_esperado, verificarFechaFin_obtenido);
        }
        [TestMethod]
        public void Test_1VerificarTotalHorasContratadasPorSemana()
        {
            Contrato contrato = new Contrato();
            contrato.TotalHorasPorSemana = 32;
            bool verificarTotalHorasContratadasPorSemana_esperado = true;
            bool verificarTotalHorasContratadasPorSemana_obtenido = contrato.VerificarTotalHorasContratadasPorSemana();
            Assert.AreEqual(verificarTotalHorasContratadasPorSemana_esperado, verificarTotalHorasContratadasPorSemana_obtenido);
        }
        [TestMethod]
        public void Test_2VerificarTotalHorasContratadasPorSemana()
        {
            Contrato contrato = new Contrato();
            contrato.TotalHorasPorSemana = 31;
            bool verificarTotalHorasContratadasPorSemana_esperado = false;
            bool verificarTotalHorasContratadasPorSemana_obtenido = contrato.VerificarTotalHorasContratadasPorSemana();
            Assert.AreEqual(verificarTotalHorasContratadasPorSemana_esperado, verificarTotalHorasContratadasPorSemana_obtenido);
        }
        [TestMethod]
        public void Test_3VerificarTotalHorasContratadasPorSemana()
        {
            Contrato contrato = new Contrato();
            contrato.TotalHorasPorSemana = 43;
            bool verificarTotalHorasContratadasPorSemana_esperado = false;
            bool verificarTotalHorasContratadasPorSemana_obtenido = contrato.VerificarTotalHorasContratadasPorSemana();
            Assert.AreEqual(verificarTotalHorasContratadasPorSemana_esperado, verificarTotalHorasContratadasPorSemana_obtenido);
        }
        [TestMethod]
        public void Test_4VerificarTotalHorasContratadasPorSemana()
        {
            Contrato contrato = new Contrato();
            contrato.TotalHorasPorSemana = 3;
            bool verificarTotalHorasContratadasPorSemana_esperado = false;
            bool verificarTotalHorasContratadasPorSemana_obtenido = contrato.VerificarTotalHorasContratadasPorSemana();
            Assert.AreEqual(verificarTotalHorasContratadasPorSemana_esperado, verificarTotalHorasContratadasPorSemana_obtenido);
        }

        [TestMethod]
        public void Test_1VerificarValorPorHora()
        {
            Contrato contrato = new Contrato();
            contrato.ValorPorHora = 9;
            bool verificarValorPorHora_esperado = false;
            bool verificarValorPorHora_obtenido = contrato.verificarValorPorHora();
            Assert.AreEqual(verificarValorPorHora_esperado, verificarValorPorHora_obtenido);
        }
        [TestMethod]
        public void Test_2VerificarValorPorHora()
        {
            Contrato contrato = new Contrato();
            contrato.ValorPorHora = 70;
            bool verificarValorPorHora_esperado = false;
            bool verificarValorPorHora_obtenido = contrato.verificarValorPorHora();
            Assert.AreEqual(verificarValorPorHora_esperado, verificarValorPorHora_obtenido);
        }

        [TestMethod]
        public void Test_3VerificarValorPorHora()
        {
            Contrato contrato = new Contrato();
            contrato.ValorPorHora = 30;
            bool verificarValorPorHora_esperado = true;
            bool verificarValorPorHora_obtenido = contrato.verificarValorPorHora();
            Assert.AreEqual(verificarValorPorHora_esperado, verificarValorPorHora_obtenido);
        }
    }
}
