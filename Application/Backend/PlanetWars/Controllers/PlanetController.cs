using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlanetWars.Services;
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
            if (result != null) return Ok(result);
            else return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }

        [Route("GetNeighbouringPlanets/{planetId}")]
        [HttpGet]
        public async Task<ActionResult> GetNeighbouringPlanets(Guid planetId)
        {
            var result = await planetService.GetRelatedPlanets(planetId);
             if (result != null) return Ok(result);
            else return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }

        [Route("DeleteAll")]
        [HttpDelete]
        public async Task<ActionResult> DeleteAll()
        {
            var result = await planetService.DeleteAll();
            if (result) return Ok(result);
            else return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}