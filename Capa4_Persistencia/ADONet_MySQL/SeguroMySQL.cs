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
    public class SeguroMySQL : ISeguro
    {
        private GestorMySQL gestorSQL;

        public SeguroMySQL(IGestorAccesoDatos gestorSQL)
        {
            this.gestorSQL = (GestorMySQL)gestorSQL;
        }
        public Seguro mostrarSeguro(int idSeguro)
        {
            Seguro seguro;
            string consultaSQL = "SELECT * FROM seguro WHERE idSeguro=" + idSeguro;

            MySqlDataReader resultadoSQL = gestorSQL.EjecutarConsulta(consultaSQL);
            if (resultadoSQL.Read())
            {
                seguro = obtenerSeguros(resultadoSQL);
                return seguro;
            }
            else
            {
                return null;
            }
        }

        public List<Seguro> obtenerSeguros()
        {
            List<Seguro> listaSeguro = new List<Seguro>();
            Seguro seguro;
            string mostrarSeguros = "SELECT * FROM seguro";
            MySqlDataReader resultadoSQL = gestorSQL.EjecutarConsulta(mostrarSeguros);
            while (resultadoSQL.Read())
            {
                    seguro = obtenerSeguros(resultadoSQL);
                    listaSeguro.Add(seguro);            
            }         
            return listaSeguro;
        }

        private Seguro obtenerSeguros(MySqlDataReader resultadoSQL)
        {
            Seguro seguro = new Seguro();
            seguro.IdSeguro = resultadoSQL.GetInt32(0);
            seguro.Afp = resultadoSQL.GetString(1);
            seguro.PorcentajeDescuento = resultadoSQL.GetInt32(2);
            return seguro;
        }
    }
}
