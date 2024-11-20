using System.IdentityModel.Tokens.Jwt;
using System.Security.Principal;
using JwtTut.Models;
using JwtTut.Services;
using JwtTut.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;

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

        [HttpPost("validate")]
        public IActionResult ValidateToken(string token)
        {
            try
            {
                bool isValid = false;

                isValid = _jwt.ValidateToken(token);

                if (isValid)
                {
                    return Ok(new { Status = "valid" });
                }

                return BadRequest("Invalid");
            }
            catch (Exception ex)
            { }

            return null;
        }

        [HttpGet("SimpleExtract")]
        public IActionResult SimpleExtract()
        {
            string token = null;

            if (Request.Headers.TryGetValue("token", out StringValues userNameValues))
                token = userNameValues.FirstOrDefault();

            if (token == null)
                return BadRequest("No Token Found");

            var isValidToken = _jwt.ValidateToken(token);

            return Ok($"Get Request invoked userName:: {isValidToken}");
        }
    }
}