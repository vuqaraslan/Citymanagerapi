﻿using CityManagerApi_aspls13.Data.Abstract;
using CityManagerApi_aspls13.Dtos;
using CityManagerApi_aspls13.Entities;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CityManagerApi_aspls13.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository? _authRepository;
        private readonly IConfiguration? _configuration;

        public AuthController(IAuthRepository? authRepository, IConfiguration? configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserForRegisterDto dto)
        {
            if (await _authRepository.UserExists(dto.Username))
            {
                ModelState.AddModelError("Username", "Username already exist !");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userToCreate = new User
            {
                Username= dto.Username
            };
            await _authRepository.Register(userToCreate, dto.Password);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserForLoginDto dto)
        {
            var user = await _authRepository.Login(dto.Username, dto.Password);
            if (user == null)
            {
                return Unauthorized();
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature),
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                    new Claim(ClaimTypes.Name,user.Username)
                })
            };

            var token=tokenHandler.CreateToken(tokenDescriptor);//
            var tokenString=tokenHandler.WriteToken(token);
            return Ok(tokenString);
        }

    }
}
