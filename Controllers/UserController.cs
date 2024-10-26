using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DOTNET_iyzico.Repository;
using DOTNET_iyzico.Interfaces;
using DOTNET_iyzico.Models;
using DOTNET_iyzico.Dtos.User;
using DOTNET_iyzico.Services;
using DOTNET_iyzico.Helpers;

namespace DOTNET_iyzico.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly TokenService _tokenService;
        public UserController(IUserRepository userRepository, TokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto userDto)
        {
            if (await _userRepository.IsEmailRegisteredAsync(userDto.Email))
            {
                return BadRequest("This email is already registered");
            }
            await _userRepository.CreateUserAsync(userDto);
            return Ok("User registered successfully");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto loginDto)
        {
            var user = await _userRepository.GetUserByEmailAsync(loginDto.Email);
            if (user == null || !PasswordHelper.VerifyPassword(loginDto.Password, user.Password))
            {
                return Unauthorized("Invalid email or password.");
            }

            var token = _tokenService.GenerateToken(user);
            return Ok(new { Token = token, User = user });
        }
    }
}