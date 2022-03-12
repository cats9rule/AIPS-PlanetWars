using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using PlanetWars.DTOs;
using PlanetWars.Data.Models;
using PlanetWars.Services;

namespace PlanetWars.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayerColorController : ControllerBase
    {
        private readonly IPlayerColorService playerColorService;

        public PlayerColorController(IPlayerColorService pcService)
        {
            this.playerColorService = pcService;
        }

        [Route("CreateColor")]
        [HttpPost]
        public async Task<ActionResult> CreateColor([FromBody] PlayerColorDto color)
        {
            var playerColor = await playerColorService.Add(color);
            return playerColor == false ? new StatusCodeResult(StatusCodes.Status500InternalServerError) : Ok(playerColor);
        }

        [Route("GetAllColors")]
        [HttpGet]
        public async Task<ActionResult> GetAllColors()
        {
            var result = await playerColorService.GetAll();
            return Ok(result);
        }
    }    
}