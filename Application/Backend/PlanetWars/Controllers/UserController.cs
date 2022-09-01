using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using PlanetWars.DTOs;
using PlanetWars.Services;

namespace PlanetWars.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("CreateUser")]
        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody] UserCreateDto user)
        {
            var result = await _userService.CreateUser(user);
            if (result != null) return Ok(result);
            else return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }

        [Route("GetAllUsers")]
        [HttpGet]
        public async Task<ActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            if (users != null) return Ok(users);
            else return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }

        [Route("GetUserById/{id}")]
        [HttpGet]
        public async Task<ActionResult> GetUserById(Guid id)
        {
            var user = await _userService.GetUser(id);

            if(user == null)
                return BadRequest();
            return Ok(user);
        }

        [Route("GetUserByUsernameAndTag/{username}/{tag}")]
        [HttpGet]
        public async Task<ActionResult> GetUserByUsername(string username, string tag)
        {
            var user = await _userService.GetUserByUsernameAndTag(username, tag);

            if(user == null)
                return BadRequest();
            return Ok(user);
        }

        [Route("UpdateUser")]
        [HttpPut]
        public async Task<ActionResult> UpdateUser([FromBody] UserDto user)
        {
            var result = await _userService.UpdateUser(user);

            if(result == false)
                return BadRequest();
            return Ok(result);
        }

        [Route("LogInUser")]
        [HttpPost]
        public async Task<ActionResult> LogInUser([FromBody] UserLoginDto user)
        {
            var result = await _userService.LogInUser(user);

            if(result == null)
                return StatusCode(401); //401 Unauthorized
            //return StatusCode(200); //200 OK
            return Ok(result);
        }

        [Route("DeleteUser/{userId}")]
        [HttpDelete]
        public async Task<ActionResult> DeleteUser(Guid userId)
        {
            var result = await _userService.DeleteUser(userId);

            if(result == false)
                return BadRequest();
            return Ok(result);
        }
    }
}