using Application.Cursos.Queries;
using Domain.Entities;
using MediatR;
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
    public class CursosController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CursosController(IMediator mediador)
        {
            _mediator = mediador;
        }

        [HttpGet]
        public async Task<ActionResult<List<Curso>>> Get()
        {
            return await _mediator.Send(new Consulta.ListaCursos());

        }

    }
}
