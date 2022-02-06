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
    public class EmpleadoMySQL: IEmpleado { 
        private GestorMySQL gestorSQL;

        public EmpleadoMySQL(IGestorAccesoDatos gestorSQL)
        {
            this.gestorSQL = (GestorMySQL)gestorSQL;
        }

        public Empleado buscarEmpleadoId(int idEmpleado)
        {
            Empleado empleado;
            string consultaSQL = "SELECT * FROM empleado WHERE idEmpleado=" + idEmpleado;

            MySqlDataReader resultadoSQL = gestorSQL.EjecutarConsulta(consultaSQL);
            if (resultadoSQL.Read())
            {
                empleado = obtenerEmpleado(resultadoSQL);
                return empleado;
            }
            else
            {
                return null;
            }
        }

        public Empleado buscarEmpleadoPorDni(int dni)
        {
            Empleado empleado;
            string consultaSQL = "SELECT * FROM empleado WHERE dni=" + dni;
            
            MySqlDataReader resultadoSQL = gestorSQL.EjecutarConsulta(consultaSQL);
            if (resultadoSQL.Read())
            {
                empleado = obtenerEmpleado(resultadoSQL);
                return empleado;
            }
            else
            {
                return null;
            }
         
        }
        private Empleado obtenerEmpleado(MySqlDataReader resultadoSQL)
        {
            Empleado empleado = new Empleado();
            empleado.IdEmpleado = resultadoSQL.GetInt32(0);
            empleado.Nombres = resultadoSQL.GetString(1);
            empleado.Dni = resultadoSQL.GetInt32(2);
            empleado.ApellidoPaterno = resultadoSQL.GetString(3);
            empleado.ApellidoMaterno = resultadoSQL.GetString(4);
            empleado.Direccion = resultadoSQL.GetString(5);
            empleado.EstadoCivil = resultadoSQL.GetString(6);
            empleado.GradoAcademico = resultadoSQL.GetString(7);
            empleado.FechaNacimiento = resultadoSQL.GetDateTime(8);
            empleado.Sexo = resultadoSQL.GetString(9);
            empleado.Telefono = resultadoSQL.GetString(10);

            return empleado;
        }
    }
}
