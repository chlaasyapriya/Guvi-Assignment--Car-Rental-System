using Assignment__Car_Rental_System.Models;
using Assignment__Car_Rental_System.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment__Car_Rental_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            if(_userService.RegisterUser(user))
                return Ok(user);
            return BadRequest("User already exists");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(User user)
        {
            var token = _userService.AuthenticateUser(user.Email, user.Password);
            if (token == null)
                return Unauthorized("Invalid Credentials");
            return Ok(new {Token=token});            
        }
    }
}
