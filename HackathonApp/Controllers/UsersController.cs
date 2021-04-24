using System;
using System.Threading.Tasks;
using HackathonApp.Dto;
using HackathonApp.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace HackathonApp.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/users")]
    public class UsersController : BaseController
    {
        private readonly IUsers _users;
        
        public UsersController(IConfiguration configuration, IUsers users) : base(configuration)
        {
            _users = users;
        }

        [HttpPost("create")]
        public async Task<ActionResult<Guid>> CreateUser(CreateUserDto data)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _users.CreteUser(data);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<LoginTokenDto>> LoginUser(LoginDto data)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _users.Login(data);
            return Ok(result);
        }


        [HttpGet]
        public async Task<ActionResult<UserDetailsDto>> GetUserDetails()
        {
            var result = await _users.GetUserDetails(UserId, Role);
            return Ok(result);
        }

    }
}