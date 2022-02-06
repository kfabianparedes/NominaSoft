using System;
using System.Collections.Generic;
using System.Text;

namespace Capa3_Dominio.Entidades
{
    public class Empleado
    {
        private int idEmpleado;
        private String nombres;
        private String apellidoMaterno;
        private String apellidoPaterno;
        private DateTime fechaNacimiento;
        private String telefono;
        private String sexo;
        private int dni;
        private String direccion;
        private String estadoCivil;
        private String gradoAcademico;

        public int IdEmpleado { get => idEmpleado; set => idEmpleado = value; }
        public string Nombres { get => nombres; set => nombres = value; }
        public string ApellidoMaterno { get => apellidoMaterno; set => apellidoMaterno = value; }
        public string ApellidoPaterno { get => apellidoPaterno; set => apellidoPaterno = value; }
        public DateTime FechaNacimiento { get => fechaNacimiento; set => fechaNacimiento = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public string Sexo { get => sexo; set => sexo = value; }
        public int Dni { get => dni; set => dni = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public string EstadoCivil { get => estadoCivil; set => estadoCivil = value; }
        public string GradoAcademico { get => gradoAcademico; set => gradoAcademico = value; }
    }
}
