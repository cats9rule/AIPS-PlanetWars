using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PlanetWars.DTOs;
using PlanetWars.Services;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace PlanetWars.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlanetController : ControllerBase
    {
        private readonly IPlanetService planetService;

        public PlanetController(IPlanetService ps)
        {
            this.planetService = ps;
        }

        [Route("GetAllPlanets")]
        [HttpGet]
        public async Task<ActionResult> GetAllPlanets()
        {
            var result = await planetService.GetAll();
            return Ok(result);
        }

        [Route("GetNeighbouringPlanets/{planetId}")]
        [HttpGet]
        public async Task<ActionResult> GetNeighbouringPlanets(Guid planetId)
        {
            var result = await planetService.GetRelatedPlanets(planetId);
            return Ok(result);
        }
    }
}