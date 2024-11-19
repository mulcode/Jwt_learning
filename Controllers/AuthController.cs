using JwtTut.Models;
using JwtTut.Services;
using JwtTut.Utils;
using Microsoft.AspNetCore.Mvc;

namespace JwtTut.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private JwtUtils _jwt { get; }
        private UserService _service { get; }


        public AuthController(JwtUtils jwt, UserService service)
        {
            _jwt = jwt;
            _service = service;
        }

        [HttpPost("login")]
        public IActionResult Login(User user)
        {
            User foundUser = _service.GetUser(user.UserName, user.Password);
            if (foundUser == null)
            {
                return BadRequest("No User Found");
            }
            string token = _jwt.GenerateJSONWebToken(user);
            return Ok(new { Token = token });
        }
    }
}