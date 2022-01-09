using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.Json;

using PlanetWars.DTOs;
using PlanetWars.Services;
using PlanetWars.Services.ConcreteServices;

namespace PlanetWars.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [Route("CreateUser")]
        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody] UserDto user)
        {
            var result = await userService.CreateUser(user);
            return Ok(result);
        }

        [Route("GetAllUsers")]
        [HttpGet]
        public async Task<ActionResult> GetAllUsers()
        {
            var users = await userService.GetAllUsers();
            return Ok(users);
        }

        [Route("GetUserById/{id}")]
        [HttpGet]
        public async Task<ActionResult> GetUserById(Guid id)
        {
            var user = await userService.GetUser(id);

            if(user == null)
                return BadRequest();
            return Ok(user);
        }

        [Route("GetUserByUsernameAndTag/{username}/{tag}")]
        [HttpGet]
        public async Task<ActionResult> GetUserByUsername(string username, string tag)
        {
            var user = await userService.GetUserByUsernameAndTag(username, tag);

            if(user == null)
                return BadRequest();
            return Ok(user);
        }

        [Route("UpdateUser")]
        [HttpPut]
        public async Task<ActionResult> UpdateUser([FromBody] UserDto user)
        {
            var result = await userService.UpdateUser(user);

            if(result == false)
                return BadRequest();
            return Ok(result);
        }

        [Route("LogInUser")]
        [HttpPost]
        public async Task<ActionResult> LogInUser([FromBody] UserDto user)
        {
            var result = await userService.LogInUser(user);

            if(result == null)
                return StatusCode(401); //401 Unauthorized
            //return StatusCode(200); //200 OK
            return Ok(result);
        }

        [Route("DeleteUser/{userId}")]
        [HttpDelete]
        public async Task<ActionResult> DeleteUser(Guid userId)
        {
            var result = await userService.DeleteUser(userId);

            if(result == false)
                return BadRequest();
            return Ok(result);
        }
    }
}