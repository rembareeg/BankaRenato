using BankaRenato.WebAPI.Data;
using BankaRenato.WebAPI.Dtos;
using BankaRenato.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BankaRenato.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IAuthRepository _rep;

        public AuthController(IConfiguration config, IAuthRepository repo)
        {
            _config = config;
            _rep = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegistration)       
        {
            //Check if user with same username exists
            if (await _rep.UserExists(userForRegistration.Username))
            {
                return BadRequest("Username already exists");
            }
            //Check if user with same email exists
            if (await _rep.EmailExists(userForRegistration.Email))
            {
                return BadRequest("Email already in use");
            }

            //Registrate user
            if (await _rep.Register(userForRegistration)) return Ok();

            return Unauthorized();

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLogin)
        {
            
            User user = await _rep.Login(userForLogin.Username, userForLogin.Password);
            
            if (user == null)
            {
                return Unauthorized();
            }

            Claim[] claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            //Generate key
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            //Generate credentials
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            //Add token descriptor
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            //Create token
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            //Return token code
            return Ok(new {
                token = tokenHandler.WriteToken(token)
            });
        }

    }
}
