using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capa3_Dominio.Entidades; 


namespace ProyectoCalidadTest
{
    [TestClass]
    public class ConceptoDeIngresoYDescuentoTest
    {
        [TestMethod]
        public void Test_1CalcularTotalConceptoDescuentos()
        {
            ConceptoDeIngresoYDescuento conceptoDeIngresoYDescuento = new ConceptoDeIngresoYDescuento();
            conceptoDeIngresoYDescuento.MontoPorHorasAusentes = 50;
            conceptoDeIngresoYDescuento.MontoPorAdelantos = 50;
            conceptoDeIngresoYDescuento.MontoPorOtrosDescuentos = 50;
            double conceptoDescuento_esperado = 150;
            double conceptoDescuento_obtenido = conceptoDeIngresoYDescuento.calcularTotalConceptoDescuentos();
            Assert.AreEqual(conceptoDescuento_esperado, conceptoDescuento_obtenido);
        }

        [TestMethod]
        public void Test1_CalcularTotalConceptoIngresos()
        {
            ConceptoDeIngresoYDescuento conceptoDeIngresoYDescuento = new ConceptoDeIngresoYDescuento();
            conceptoDeIngresoYDescuento.MontoPorHorasExtra = 15;
            conceptoDeIngresoYDescuento.MontoReintegros = 15;
            conceptoDeIngresoYDescuento.MontoPorOtrosIngresos = 15;
            double calcularTotalConceptoIngresos_esperado = 45;
            double calcularTotalConceptoIngresos_obtenido = conceptoDeIngresoYDescuento.calcularTotalConceptoIngresos();
            Assert.AreEqual(calcularTotalConceptoIngresos_esperado, calcularTotalConceptoIngresos_obtenido);
        }
    }
}
