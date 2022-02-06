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

    public class PagoMySQL : IPago

    {
        private GestorMySQL gestorSQL;

        public PagoMySQL(IGestorAccesoDatos gestorSQL)
        {
            this.gestorSQL = (GestorMySQL)gestorSQL;
        }

        public List<Pago> listarPagosRealizadoPorPeriodo(int idPeriodo)
        {
            List<Pago> ListaPagosRealizados = new List<Pago>();
            Pago pago = new Pago();
            string query = "select * from pago pag inner join contrato cont on (pag.idContrato = cont.idContrato) inner join empleado emp on (cont.idEmpleado = emp.idEmpleado)inner join periododepago pdp on (pag.idPeriodoDePago = pdp.idperiodoDePago)inner join conceptosdeingresoydescuento cid on (pag.idConcepto = cid.idConcepto) where pag.idPeriodoDePago  ="+idPeriodo;
            MySqlDataReader resultadoSQL = gestorSQL.EjecutarConsulta(query);
            while (resultadoSQL.Read())
            {
                pago = obtenerPago(resultadoSQL);
                ListaPagosRealizados.Add(pago);
            }
            return ListaPagosRealizados;
        }
        public Pago obtenerPago(MySqlDataReader resultadoSQL) {
            Pago pago = new Pago();
            Contrato contrato = new Contrato();
            PeriodoDePago periodo = new PeriodoDePago();
            Empleado emp = new Empleado();
            ConceptoDeIngresoYDescuento concepto = new ConceptoDeIngresoYDescuento();
            pago.IdPago = resultadoSQL.GetInt32(0);//21
            pago.TotalDeHoras = resultadoSQL.GetInt32(4);
            pago.ValorPorHora = resultadoSQL.GetInt32(5);
            emp.Nombres = resultadoSQL.GetString(21);
            emp.Dni = resultadoSQL.GetInt32(22);
            emp.ApellidoPaterno = resultadoSQL.GetString(23);
            emp.ApellidoMaterno = resultadoSQL.GetString(24);
            periodo.FechaInicio = resultadoSQL.GetDateTime(11);
            periodo.FechaFin = resultadoSQL.GetDateTime(12);//14
            contrato.TotalHorasPorSemana = resultadoSQL.GetInt32(14);
            concepto.MontoPorAdelantos = resultadoSQL.GetFloat(35);
            concepto.MontoPorHorasAusentes = resultadoSQL.GetFloat(36);
            concepto.MontoPorHorasExtra = resultadoSQL.GetFloat(37);
            concepto.MontoPorOtrosDescuentos = resultadoSQL.GetFloat(38);
            concepto.MontoPorOtrosIngresos = resultadoSQL.GetFloat(39);
            concepto.MontoReintegros = resultadoSQL.GetFloat(40);
            contrato.Empleado = emp;
            pago.Contrato = contrato;
            pago.ConceptoDeIngresoYDescuento = concepto;
            pago.PeriodoDePago = periodo; 
            return pago;

        }

        public bool realizarPagos(Pago pago)
        {
            string InsertarPagosSQL;
            InsertarPagosSQL = "insert into pago (fechaDePago,montoAsignacionFamiliar,PorcentajeDescuentoAfp,totalDeHoras,valorPorHora,idContrato,idConcepto,idPeriodoDePago)" +
                "values(@fechaDePago,@montoAsignacionFamiliar,@PorcentajeDescuentoAfp,@totalDeHoras,@valorPorHora,@idContrato,@idConcepto,@idPeriodoDePago)";
            MySqlCommand comando;

                comando = gestorSQL.ObtenerComandoSQL(InsertarPagosSQL);
                comando.Parameters.AddWithValue("@fechaDePago", pago.FechaDePago);
                comando.Parameters.AddWithValue("@montoAsignacionFamiliar", pago.MontoAsignacionFamiliar);
                comando.Parameters.AddWithValue("@PorcentajeDescuentoAfp", pago.PorcentajeDescuentoAfp);
                comando.Parameters.AddWithValue("@totalDeHoras", pago.TotalDeHoras);
                comando.Parameters.AddWithValue("@valorPorHora", pago.ValorPorHora);
                comando.Parameters.AddWithValue("@idContrato", pago.Contrato.IdContrato);
                comando.Parameters.AddWithValue("@idConcepto", pago.ConceptoDeIngresoYDescuento.IdConcepto);
                comando.Parameters.AddWithValue("@idPeriodoDePago", pago.PeriodoDePago.IdPeriodo);

            if (comando.ExecuteNonQuery() > 0)
                return true;
            else return false;
        }


    }
}
