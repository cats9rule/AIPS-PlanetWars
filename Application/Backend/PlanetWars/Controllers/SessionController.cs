using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using PlanetWars.DTOs;
using PlanetWars.Services;

namespace PlanetWars.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService sessionService;
        private readonly IGalaxyService galaxyService;
        private readonly IPlayerService playerService;
        public SessionController(ISessionService sService, IGalaxyService gService, IPlayerService pService)
        {
            this.sessionService = sService;
            this.galaxyService = gService;
            this.playerService = pService;
        }

        [Route("CreateGame")]
        [HttpPost]
        public async Task<ActionResult> CreateGame([FromBody] CreateGameDto createGameDto)
        {
            var result = await sessionService.CreateSession(createGameDto);
            //var player = await playerService.CreatePlayer(createGameDto.UserId, 0, session.ID);
            //var galaxy = await galaxyService.CreateGalaxy(createGameDto.PlanetCount, createGameDto.ResourcePlanetRatio, session.ID);

            //var result = await sessionService.InitializeSession(session, galaxy.ID, player.ID);
            // var player = await playerService.CreatePlayer(createGameDto.UserId, 0);
            // var galaxy = await galaxyService.CreateGalaxy(createGameDto.PlanetCount, createGameDto.ResourcePlanetRatio);
            // var result = await sessionService.CreateSession(createGameDto.Name, createGameDto.Password, createGameDto.MaxPlayers, galaxy, player);

            return Ok(result);
        }

        [Route("AddPlayer")]
        [HttpPut]
        public async Task<ActionResult> AddPlayer([FromBody] PlayerDto playerDto)
        {
            var session = await sessionService.GetById(playerDto.SessionID);
            var player = await playerService.CreatePlayer(playerDto.UserID, session.PlayerCount, session.ID);
            var result = await sessionService.AddPlayer(session, player);

            return Ok(result);
        }

        [Route("GetSession/{id}")]
        [HttpGet]
        public async Task<ActionResult> GetSession(Guid id)
        {
            var result = await sessionService.GetById(id);
            return Ok(result);
        }

        [Route("GetAllSessions")]
        [HttpGet]
        public async Task<ActionResult> GetAllSessions()
        {
            var result = await sessionService.GetAllSessions();
            return Ok(result);
        }

        [Route("GetByName/{name}")]
        [HttpGet]
        public async Task<ActionResult> GetByName(string name)
        {
            var result = await sessionService.GetByName(name);
            return Ok(result);
        }

        [Route("UpdateSession")]
        [HttpPut]
        public async Task<ActionResult> UpdateSession([FromBody] UpdateSessionDto sessionDto)
        {
            var result = await sessionService.Update(sessionDto);
            if(result == false)
                return BadRequest();
            return Ok(result);
        }

        [Route("DeleteSession/{sessionId}")]
        [HttpDelete]
        public async Task<ActionResult> DeleteSession(Guid sessionId)
        {
            var result = await sessionService.Delete(sessionId);
            if(result == false)
                return BadRequest();
            return Ok(result);
        }

    }
}