using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using PlanetWars.DTOs;
using PlanetWars.Services;

namespace PlanetWars.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService playerService;
        public PlayerController(IPlayerService playerService)
        {
            this.playerService = playerService;
        }

        [Route("CreatePlayer/{sessionId}")]
        [HttpPost]
        public async Task<ActionResult> CreatePlayer(Guid sessionId, [FromBody] PlayerDto player)
        {
            var result = await playerService.CreatePlayer(player.UserID, player.TurnIndex, sessionId);
            return Ok(result);
        }

        [Route("GetAllPlayers")]
        [HttpGet]
        public async Task<ActionResult> GetAllPlayers()
        {
            var result = await playerService.GetAll();
            return Ok(result);
        }

        [Route("GetPlayerById/{id}")]
        [HttpGet]
        public async Task<ActionResult> GetPlayerById(Guid id)
        {
            var result = await playerService.GetByUserId(id);

            if(result == null)
                return BadRequest();
            return Ok(result);
        }

        [Route("GetPlayerByUserId/{userId}")]
        [HttpGet]
        public async Task<ActionResult> GetPlayerByUserId(Guid userId)
        {
            var result = await playerService.GetByUserId(userId);
            if(result == null)
                return BadRequest();
            return Ok(result);
        }

        [Route("GetPlayerByUsernameAndTag/{username}/{tag}")]
        [HttpGet]
        public async Task<ActionResult> GetPlayerByUsernameAndTag(string username, string tag)
        {
            var result = await playerService.GetByUsernameAndTag(username, tag);
            if (result == null)
                return BadRequest();
            return Ok(result);
        }

        [Route("UpdatePlayer")]
        [HttpPut]
        public async Task<ActionResult> UpdatePlayer([FromBody] PlayerDto player)
        {
            var result = await playerService.Update(player);
            if (result == false)
                return BadRequest();
            return Ok(result);
        }

        [Route("DeletePlayer/{playerId}")]
        [HttpDelete]
        public async Task<ActionResult> DeletePlayer(Guid playerId)
        {
            var result = await playerService.Delete(playerId);
            if (result == false)
                return BadRequest();
            return Ok(result);
        }
    }
}