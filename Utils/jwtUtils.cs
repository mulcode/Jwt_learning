using System.IdentityModel.Tokens.Jwt;
using System.Text;
using JwtTut.Models;
using Microsoft.IdentityModel.Tokens;

namespace JwtTut.Utils
{

    public class JwtUtils
    {
        private readonly IConfiguration _configuration;

        public JwtUtils(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public string GenerateJSONWebToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
              _configuration["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

             var rtoken =  new JwtSecurityTokenHandler().WriteToken(token);

            return rtoken;
        }
    }
}