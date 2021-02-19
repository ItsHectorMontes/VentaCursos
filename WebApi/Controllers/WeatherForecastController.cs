using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ApplicationDbContextSeed contextSeed;

      public WeatherForecastController(ApplicationDbContextSeed _contextSeed)
        {
            this.contextSeed = _contextSeed;

        }
        [HttpGet]
        public IEnumerable<Curso> Get()
        {
            return contextSeed.Curso.ToList();
        }
    }
}
