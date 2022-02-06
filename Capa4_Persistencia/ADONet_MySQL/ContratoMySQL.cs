using Capa3_Dominio.Contratos;
using Capa3_Dominio.Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa4_Persistencia.ADONet_MySQL
{
    public class ContratoMySQL: IContrato
    {
        private GestorMySQL gestorSQL;

        public ContratoMySQL(IGestorAccesoDatos gestorSQL)
        {
            this.gestorSQL = (GestorMySQL)gestorSQL;
        }

        public int crearContrato(Contrato contrato)
        {
            string InsertarContratoSQL;
            InsertarContratoSQL = "insert into contrato (esVigente,fechaInicio,fechaFin,tieneasignacionFamiliar,totalHorasPorSemana,valorPorHora,estaAnulado,cargo,idSeguro,idEmpleado)" +
                "values(@esvigente,@fechaInicio,@fechaFin,@tieneasignacionfamiliar,@totalhorasporsemana,@valorporhora,@estaanulado,@cargo,@idseguro,@idempleado)";

            MySqlCommand comando;
            comando = gestorSQL.ObtenerComandoSQL(InsertarContratoSQL);
            comando.Parameters.AddWithValue("@esvigente", contrato.EsVigente);
            comando.Parameters.AddWithValue("@estaanulado", contrato.EstaAnulado);
            comando.Parameters.AddWithValue("@fechaInicio", contrato.FechaInicial);
            comando.Parameters.AddWithValue("@fechaFin", contrato.FechaFinal);
            comando.Parameters.AddWithValue("@cargo", contrato.Cargo);
            comando.Parameters.AddWithValue("@tieneasignacionfamiliar", contrato.TieneAsignacionFamiliar);
            comando.Parameters.AddWithValue("@totalhorasporsemana", contrato.TotalHorasPorSemana);
            comando.Parameters.AddWithValue("@valorporhora", contrato.ValorPorHora);
            comando.Parameters.AddWithValue("@idempleado", contrato.Empleado.IdEmpleado);
            comando.Parameters.AddWithValue("@idseguro", contrato.Seguro.IdSeguro);
            

            if (comando.ExecuteNonQuery() > 0)
            {
                return 1;
            }
            else
                return 0;
                
        }

        public List<Contrato> buscarContratoPorIdEmpleado(int idEmpleado)
        {
            List<Contrato> listaContratos = new List<Contrato>();
            Contrato contrato;
            string consultaSQL = "select * from contrato where idEmpleado =" + idEmpleado;

            MySqlDataReader resultadoSQL = gestorSQL.EjecutarConsulta(consultaSQL);
            while (resultadoSQL.Read())
            {
                contrato = obtenerContrato(resultadoSQL);
                listaContratos.Add(contrato);
            }
            return listaContratos;
        }

        public int editarContrato(Contrato contrato)
        {

            string actualizarContrato = "UPDATE contrato SET cargo = @cargo ,fechaInicio = @fechaInicio,fechaFin = @fechaFin,totalHorasPorSemana = @totalHorasPorSemana,valorPorHora =@valorPorHora,tieneAsignacionFamiliar =@tieneAsignacionFamiliar,idSeguro =@idSeguro WHERE idContrato =@idContrato";
                MySqlCommand comando;
                comando = gestorSQL.ObtenerComandoSQL(actualizarContrato);
                comando.Parameters.AddWithValue("@idContrato", contrato.IdContrato);
                comando.Parameters.AddWithValue("@cargo",contrato.Cargo);
                comando.Parameters.AddWithValue("@fechaInicio", contrato.FechaInicial);
                comando.Parameters.AddWithValue("@fechaFin", contrato.FechaFinal);
                comando.Parameters.AddWithValue("@totalHorasPorSemana", contrato.TotalHorasPorSemana);
                comando.Parameters.AddWithValue("@valorPorHora", contrato.ValorPorHora);
                comando.Parameters.AddWithValue("@tieneAsignacionFamiliar", contrato.TieneAsignacionFamiliar);
                comando.Parameters.AddWithValue("@idSeguro", contrato.Seguro.IdSeguro);
            if (comando.ExecuteNonQuery() > 0)
            {
                return 1;
            }
            else
                return 0;
        }
        

        public List<Contrato> listarContratosPorPeriodo(int idPeriodo)
        {
            List<Contrato> listaContratos = new List<Contrato>();
            Contrato contrato;
            PeriodoDePago periodo = new PeriodoDePago();
             string consultaSQL1 = "select * from contrato con inner join empleado emp on(con.idEmpleado = emp.idEmpleado) inner join seguro seg on (con.idSeguro = seg.idSeguro) inner join conceptosdeingresoydescuento cid on(con.idContrato = cid.idContrato) where cid.idPeriodoDePago = " + idPeriodo; 

            MySqlDataReader resultadoSQL = gestorSQL.EjecutarConsulta(consultaSQL1);
            while (resultadoSQL.Read())
            {
                contrato = obtenerContrato2(resultadoSQL);
                periodo.IdPeriodo = idPeriodo;
                contrato.ConceptoDeIngresoYDescuento.PeriodoDePago = periodo;
                listaContratos.Add(contrato);
            }
            return listaContratos;
        }
        private Contrato obtenerContrato(MySqlDataReader resultadoSQL)
        {
            Contrato contrato = new Contrato();
            Empleado empleado = new Empleado();
            Seguro seguro = new Seguro();
            contrato.IdContrato = resultadoSQL.GetInt32(0);
            contrato.EsVigente = resultadoSQL.GetBoolean(1);
            contrato.FechaInicial = resultadoSQL.GetDateTime(2);
            contrato.FechaFinal = resultadoSQL.GetDateTime(3);
            contrato.TieneAsignacionFamiliar = resultadoSQL.GetBoolean(4);
            contrato.TotalHorasPorSemana = resultadoSQL.GetInt32(5);
            contrato.ValorPorHora = resultadoSQL.GetInt32(6);
            contrato.EstaAnulado = resultadoSQL.GetBoolean(7);
            contrato.Cargo = resultadoSQL.GetString(8);
            seguro.IdSeguro = resultadoSQL.GetInt32(9);
            empleado.IdEmpleado = resultadoSQL.GetInt32(10);
            contrato.Empleado = empleado;
            contrato.Seguro = seguro;
            return contrato;
        }
        private Contrato obtenerContrato2(MySqlDataReader resultadoSQL)
        {
            Contrato contrato = new Contrato();
            Empleado empleado = new Empleado();
            Seguro seguro = new Seguro();
            contrato.IdContrato = resultadoSQL.GetInt32(0);
            contrato.EsVigente = resultadoSQL.GetBoolean(1);
            contrato.FechaInicial = resultadoSQL.GetDateTime(2);
            contrato.FechaFinal = resultadoSQL.GetDateTime(3);
            contrato.TieneAsignacionFamiliar = resultadoSQL.GetBoolean(4);
            contrato.TotalHorasPorSemana = resultadoSQL.GetInt32(5);
            contrato.ValorPorHora = resultadoSQL.GetInt32(6);
            contrato.EstaAnulado = resultadoSQL.GetBoolean(7);
            contrato.Cargo = resultadoSQL.GetString(8);
            seguro.IdSeguro = resultadoSQL.GetInt32(9);
            empleado.IdEmpleado = resultadoSQL.GetInt32(10);
            empleado.IdEmpleado = resultadoSQL.GetInt32(11);
            empleado.Nombres = resultadoSQL.GetString(12);
            empleado.Dni = resultadoSQL.GetInt32(13);
            empleado.ApellidoPaterno = resultadoSQL.GetString(14);
            empleado.ApellidoMaterno = resultadoSQL.GetString(15);
            seguro.Afp = resultadoSQL.GetString(23);
            seguro.PorcentajeDescuento = resultadoSQL.GetInt32(24);
            contrato.Empleado = empleado;
            contrato.Seguro = seguro;
            return contrato;
        }

        public int anularContrato(int idContrato)
        {
            string anularContrato = "UPDATE contrato SET estaAnulado = 1  WHERE idContrato =@idContrato";
            MySqlCommand comando;
            comando = gestorSQL.ObtenerComandoSQL(anularContrato);
            comando.Parameters.AddWithValue("@idContrato", idContrato);
            if (comando.ExecuteNonQuery() > 0)
            {
                return 1;
            }
            else
                return 0;
        }
    }
}
