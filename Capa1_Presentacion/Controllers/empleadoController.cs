using Capa1_Presentacion.Filters;
using Capa2_Aplicacion.Servicios;
using Capa3_Dominio.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capa1_Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class empleadoController : ControllerBase
    {
        [HttpGet("{dni}")]
        public ActionResult<Empleado> retornarEmpleado(int dni)
        {
            Empleado empleado = new Empleado();
            Mensaje_Error mensaje_error = new Mensaje_Error();
            GestionarContratoServicio gestionarContratoServicio = new GestionarContratoServicio();
            empleado = gestionarContratoServicio.buscarEmpleado(dni);
            if(empleado== null)
            {
                mensaje_error = new Mensaje_Error("No existe el empleado");
                return BadRequest(mensaje_error);

            }
            
            return Ok(empleado);
        }
        [HttpGet]
        [Route("id/{id}")]
        public ActionResult buscarEmpleadoPorID(int id)
        {
            Empleado empleado = new Empleado();
            Mensaje_Error mensaje_error = new Mensaje_Error();
            GestionarContratoServicio gestionarContratoServicio = new GestionarContratoServicio();
            empleado = gestionarContratoServicio.buscarEmpleadoPorId(id);
            
            if (empleado == null)
            {
                mensaje_error = new Mensaje_Error("El usuario no ha sido encontrado");
                return BadRequest(mensaje_error);
            }
            return Ok(empleado); ;

        }
    }
}
