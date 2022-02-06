using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capa3_Dominio.Entidades;
using Capa2_Aplicacion.Servicios;

namespace Capa1_Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class seguroController: ControllerBase
    {
        [HttpGet]
        public List<Seguro> mostrarSeguros()
        {
            List<Seguro> listarSeguro = new List<Seguro>();
            GestionarContratoServicio gestionarContratoServicio = new GestionarContratoServicio();
            listarSeguro = gestionarContratoServicio.obtenerTodoLosSeguro();
            return listarSeguro;
        }
        [HttpGet]
        [Route("id/{id}")]
        public Seguro buscarSeguroPorId(int id)
        {
            Seguro seguro = new Seguro();
            GestionarContratoServicio gestionarContratoServicio = new GestionarContratoServicio();
            seguro = gestionarContratoServicio.buscarSeguroPorId(id);
            if (seguro == null)
            {
                throw new("No se encontró el seguro en la base de datos");
            }
            return seguro;

        }
    }
}
