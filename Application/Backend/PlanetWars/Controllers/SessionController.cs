using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PlanetWars.DTOs;
using PlanetWars.Services;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace PlanetWars.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService sessionService;
        private readonly IGalaxyService galaxyService;
        private readonly IPlanetService planetService;
        private readonly IPlayerService playerService;
        public SessionController(ISessionService sService, IGalaxyService gService, IPlayerService pService, IPlanetService planetService)
        {
            this.sessionService = sService;
            this.galaxyService = gService;
            this.playerService = pService;
            this.planetService = planetService;
        }

        [Route("CreateGame")]
        [HttpPost]
        public async Task<ActionResult> CreateGame([FromBody] CreateGameDto createGameDto)
        {
            var sessionDto = await sessionService.CreateSession(createGameDto);
            List<PlanetDto> planets = new List<PlanetDto>(await planetService.CreatePlanets(createGameDto, sessionDto.GalaxyID));
            return sessionDto == null ? new StatusCodeResult(StatusCodes.Status500InternalServerError) : Ok(sessionDto);
        }

        // [Route("AddPlayer")]
        // [HttpPut]
        // public async Task<ActionResult> AddPlayer([FromBody] PlayerDto playerDto)
        // {
        //     var session = await sessionService.GetById(playerDto.SessionID);
        //     var player = await playerService.CreatePlayer(playerDto.UserID, session.PlayerCount, session.ID);
        //     var result = await sessionService.AddPlayer(session.ID, player);
        //     return Ok(result);
        // }

        [Route("JoinGame")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> JoinGame([FromBody] JoinGameDto joinGameDto)
        {
            var session = await sessionService.GetByNameAndCode(joinGameDto.SessionName, joinGameDto.GameCode);
            if (session != null) 
            {
                var player = await playerService.CreatePlayer(joinGameDto.UserID, session.PlayerCount, session.ID);
                var result = await sessionService.AddPlayer(session.ID, player);
                if (result != null) return Ok(result);
                else return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return new StatusCodeResult(StatusCodes.Status404NotFound);
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

        [Route("GetByName/{name}/{code}")]
        [HttpGet]
        public async Task<ActionResult> GetByNameAndCode(string name, string code)
        {
            var result = await sessionService.GetByNameAndCode(name, code);
            return Ok(result);
        }

        [Route("UpdateSession")]
        [HttpPut]
        public async Task<ActionResult> UpdateSession([FromBody] UpdateSessionDto sessionDto)
        {
            var result = await sessionService.Update(sessionDto);
            if (result == false)
                return BadRequest();
            return Ok(result);
        }

        [Route("DeleteSession/{sessionId}")]
        [HttpDelete]
        public async Task<ActionResult> DeleteSession(Guid sessionId)
        {
            var result = await sessionService.Delete(sessionId);
            if (result == false)
                return BadRequest();
            return Ok(result);
        }

        [Route("LeaveGame")]
        [HttpPut]
        public async Task<ActionResult> LeaveGame(LeaveGameDto dto)
        {
            if (dto.PlayerID.ToString() == "" || dto.SessionID.ToString() == "")
            {
                return BadRequest(false);
            }
            try
            {
                var result = await sessionService.LeaveGame(dto);
                if (result)
                {
                    var session = await sessionService.GetById(dto.SessionID);
                    if (session.PlayerCount == 0)
                    {
                        result = await sessionService.Delete(dto.SessionID);
                    }
                }
                return result ? Ok(result) : new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            catch (DbUpdateException e)
            {
                Console.Error.WriteLine(e.Message);
                Console.Error.WriteLine(e.StackTrace);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        //TODO: implement methods for exchanging game state



    }
}