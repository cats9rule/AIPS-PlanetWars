using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlanetWars.Data.Models;
using PlanetWars.Services;

namespace PlanetWars.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameMapController : ControllerBase
    {
        private readonly IGameMapService _gameMapService;

        public GameMapController(IGameMapService gameMapService)
        {
            _gameMapService = gameMapService;
        }

        // works
        [Route("CreateGameMap")]
        [HttpPost]
        public async Task<ActionResult> CreateGameMap([FromBody] GameMap gameMap)
        {
            var result = await _gameMapService.AddGameMap(gameMap);
            if (result) return Ok(result);
            else return BadRequest(result);
        }

        // works
        [Route("GetAllGameMaps")]
        [HttpGet]
        public async Task<ActionResult> GetAllGameMaps()
        {
            var maps = await _gameMapService.GetAll();
            return Ok(maps);
        }

    }
}