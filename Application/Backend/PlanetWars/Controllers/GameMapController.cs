using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlanetWars.Data.Models;
using PlanetWars.DTOs;
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
            else return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }

        // works
        [Route("GetAllGameMaps")]
        [HttpGet]
        public async Task<ActionResult> GetAllGameMaps()
        {
            List<GameMapDto> maps = new List<GameMapDto>(await _gameMapService.GetAll());
            return maps.Count > 0 ? Ok(maps) : NotFound(maps);
        }

    }
}