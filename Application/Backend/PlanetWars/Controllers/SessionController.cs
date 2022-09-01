using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PlanetWars.DTOs;
using PlanetWars.Services;
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
            List<PlanetDto> planets = new List<PlanetDto>(await planetService.CreatePlanets(createGameDto, sessionDto.Galaxy.ID, sessionDto.ID));
            if (sessionDto != null)
            {
                sessionDto.Galaxy.Planets = planets;
                sessionDto = await playerService.SpawnPlayer(sessionDto.ID, sessionDto.Players[sessionDto.PlayerCount - 1].ID);
                if (sessionDto != null)
                {
                    //sessionDto.Galaxy.Planets = sessionDto.Galaxy.Planets.OrderBy(p => p.IndexInGalaxy).ToList();
                    return Ok(sessionDto);
                }
            }
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }

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
                //var player = await playerService.CreatePlayer(joinGameDto.UserID, session.PlayerCount, session.ID);
                var result = await sessionService.AddPlayer(session.ID, joinGameDto.UserID);
                if (result != null)
                {
                    result = await playerService.SpawnPlayer(result.ID, result.Players[result.PlayerCount - 1].ID);
                    if (result != null)
                    {
                        //result.Galaxy.Planets = result.Galaxy.Planets.OrderBy(p => p.IndexInGalaxy).ToList();
                        return Ok(result);
                    }
                }
                else return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return new StatusCodeResult(StatusCodes.Status404NotFound);
        }

        // TODO: proveriti da li radi za akcije sa Movement i Attack (radi za Placement)
        [Route("PlayMove")]
        [HttpPut]
        public async Task<IActionResult> PlayMove([FromBody] TurnDto turn)
        {
            try
            {
                var result = await sessionService.PlayMove(turn);
                if (result) return Ok();
                else return Problem(detail: "Could not notify session members.");
            }
            catch (Exception e)
            {
                Console.WriteLine("\n Exception in PlayMove: " + e.Message + "\n");
                return Problem(detail: e.Message);
            }
        }

        [Route("GetSession/{id}")]
        [HttpGet]
        public async Task<ActionResult> GetSession(Guid id)
        {
            var result = await sessionService.GetById(id);
            if (result != null) return Ok(result);
            else return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }

        [Route("GetAllSessions")]
        [HttpGet]
        public async Task<ActionResult> GetAllSessions()
        {
            var result = await sessionService.GetAllSessions();
            if (result != null) return Ok(result);
            else return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }

        [Route("GetByName/{name}/{code}")]
        [HttpGet]
        public async Task<ActionResult> GetByNameAndCode(string name, string code)
        {
            var result = await sessionService.GetByNameAndCode(name, code);
            if (result != null) return Ok(result);
            else return new StatusCodeResult(StatusCodes.Status500InternalServerError);
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

        [Route("DeleteAll")]
        [HttpDelete]
        public async Task<ActionResult> DeleteAllSessions()
        {
            var result = await sessionService.DeleteAll();
            if (result == false)
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            return Ok(result);
        }

        [Route("LeaveGame")]
        [HttpPut]
        public async Task<ActionResult> LeaveGame(LeaveGameDto dto)
        {
            if (dto.PlayerID == "" || dto.SessionID == "")
            {
                return BadRequest(false);
            }
            try
            {
                var result = await sessionService.LeaveGame(dto);
                if (result)
                {
                    var session = await sessionService.GetById(new Guid(dto.SessionID));
                    if (session.PlayerCount == 0)
                    {
                        result = await sessionService.Delete(new Guid(dto.SessionID));
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
    }
}