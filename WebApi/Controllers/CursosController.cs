using Application.Cursos.Queries;
using Application.Cursos.Queries.DtoCourse;
using Domain.Entities;
using Infrastructure.DapperConexion.Paginacion;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    //endpoint
   
    [Route("api/[controller]")]
    [ApiController]
    public class CursosController : MiControllerBase
    {       

        [HttpGet]        
        public async Task<ActionResult<List<CursoDto>>> Get()
        {
            return await Mediator.Send(new Consulta.ListaCursos());
        }

        /// <summary>
        /// curso por id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CursoDto>> Detalle(Guid id)
        {
            return await Mediator.Send(new ConsultaId.CursoUnico { Id = id });
        }
        /// <summary>
        /// crear nuevo curso
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data)
        {
            return await Mediator.Send(data);
        }

        /// <summary>
        /// editar curso
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>>Editar(Guid id, Editar.Ejecuta data)
        {
            data.CursoId = id;
            return await Mediator.Send(data);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Eliminar(Guid id)
        {
            return await Mediator.Send(new Eliminar.Ejecuta { Id = id });
        }
        //paginacion
        [HttpPost("report")]
        public async Task<ActionResult<PaginacionModel>> Report(PaginacionCurso.Ejecuta data)
        {
            return await Mediator.Send(data);
        }

    }
}
