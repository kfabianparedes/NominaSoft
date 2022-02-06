using Capa3_Dominio.Contratos;
using Capa3_Dominio.Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa4_Persistencia.ADONet_MySQL
{
    public class PeriodoDePagoMySQL : IPeriodoDePago
    {
        private GestorMySQL gestorSQL;

        public PeriodoDePagoMySQL(IGestorAccesoDatos gestorSQL)
        {
            this.gestorSQL = (GestorMySQL)gestorSQL;
        }
        public List<PeriodoDePago> obtenerPeriodosDePago()
        {
            List<PeriodoDePago> listaPeriodos = new List<PeriodoDePago>();
            PeriodoDePago periodoDePago;
            string mostrarSeguros = "SELECT * FROM periododepago ";
            MySqlDataReader resultadoSQL = gestorSQL.EjecutarConsulta(mostrarSeguros);
            while (resultadoSQL.Read())
            {
                periodoDePago = obtenerPeriodosDePago(resultadoSQL);
                listaPeriodos.Add(periodoDePago);
            }
            return listaPeriodos;
        }
        public bool cambiarProcesado(int idPeriodo)
        {
            string procesarPeriodo = "UPDATE periododepago SET estado = 0 where = @idPeriodo";
            MySqlCommand comando;
            comando = gestorSQL.ObtenerComandoSQL(procesarPeriodo);
            comando.Parameters.AddWithValue("@idPeriodo", idPeriodo);

            if (comando.ExecuteNonQuery() > 0)
            {
                return true;
            }
            else
                return false;
        }
        private PeriodoDePago obtenerPeriodosDePago(MySqlDataReader resultadoSQL)
        {
            PeriodoDePago periodoDePago = new PeriodoDePago();
            periodoDePago.IdPeriodo = resultadoSQL.GetInt32(0);
            periodoDePago.Estado = resultadoSQL.GetBoolean(1);
            periodoDePago.FechaFin = resultadoSQL.GetDateTime(2);
            periodoDePago.FechaInicio = resultadoSQL.GetDateTime(3);
            return periodoDePago;
        }
    }
}
