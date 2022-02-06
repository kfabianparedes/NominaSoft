using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa3_Dominio.Contratos;
using Capa3_Dominio.Entidades;
using MySql.Data.MySqlClient;

namespace Capa4_Persistencia.ADONet_MySQL
{
    public class ConceptosMySQL : IConceptos
    {
        private GestorMySQL gestorSQL;

        public ConceptosMySQL(IGestorAccesoDatos gestorSQL)
        {
            this.gestorSQL = (GestorMySQL)gestorSQL;
        }

        public ConceptoDeIngresoYDescuento listarConceptosYContratosPorIdPeriodoPago(int idPeriodo, int idContrato)
        {
            ConceptoDeIngresoYDescuento concepto = new ConceptoDeIngresoYDescuento();
            string consulta = "select * from conceptosdeingresoydescuento con inner join periododepago pdp on (con.idPeriodoDePago = pdp.idperiodoDePago) where con.idContrato =  '" + idContrato + "' and pdp.idperiodoDePago= '" + idPeriodo + "'";

            MySqlDataReader resultadoSQL = gestorSQL.EjecutarConsulta(consulta);
            while (resultadoSQL.Read())
            {
                concepto = obtenerConcepto(resultadoSQL);
            }
            return concepto;
        }
        public ConceptoDeIngresoYDescuento obtenerConcepto(MySqlDataReader resultadoSQL) {
            ConceptoDeIngresoYDescuento concepto = new ConceptoDeIngresoYDescuento();
            PeriodoDePago periodo = new PeriodoDePago();
            concepto.IdConcepto = resultadoSQL.GetInt32(0);
            concepto.MontoPorAdelantos = resultadoSQL.GetFloat(1);
            concepto.MontoPorHorasAusentes = resultadoSQL.GetFloat(2);
            concepto.MontoPorHorasExtra = resultadoSQL.GetFloat(3);
            concepto.MontoPorOtrosDescuentos = resultadoSQL.GetFloat(4);
            concepto.MontoPorOtrosIngresos = resultadoSQL.GetFloat(5);
            concepto.MontoReintegros = resultadoSQL.GetFloat(6);
            periodo.IdPeriodo = resultadoSQL.GetInt32(9);
            periodo.Estado = resultadoSQL.GetBoolean(10);
            periodo.FechaFin = resultadoSQL.GetDateTime(11);
            periodo.FechaInicio = resultadoSQL.GetDateTime(12);
            concepto.PeriodoDePago = periodo; 

            return concepto; 
        }
    }
}
