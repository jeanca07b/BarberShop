using BarberShop.API.Services;
using BarberShop.Application.DTOs.Auth;
using BarberShop.Domain.Entities;
using BarberShop.Domain.Enums;
using BarberShop.Domain.Repositories;
using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;

namespace BarberShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public AuthController(
            IUserRepository userRepository,
            JwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var existingUser = await _userRepository.GetByEmailAsync(request.Email);
            if (existingUser != null)
                return BadRequest("User already exists.");

            if (!IsValidPassword(request.Password))
                return BadRequest("Password must be at least 8 characters long and include an uppercase letter, a number, and a special character.");

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);


            var user = new User(
                request.Email,
                passwordHash,
                request.Role
            );

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            return Created("", new { user.Id, user.Email });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user == null)
                return Unauthorized("Invalid credentials.");

            var isPasswordValid = BCrypt.Net.BCrypt.Verify(
                request.Password,
                user.PasswordHash
            );

            if (!isPasswordValid)
                return Unauthorized("Invalid credentials.");

            var token = _jwtTokenGenerator.GenerateToken(
                user.Id,
                user.Email,
                user.Role
            );

            return Ok(new AuthResponse
            {
                Token = token,
                Email = user.Email,
                Role = user.Role.ToString()
            });
        }

        private bool IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            if (password.Length < 8)
                return false;

            bool hasUpperCase = password.Any(char.IsUpper);
            bool hasDigit = password.Any(char.IsDigit);
            bool hasSpecialChar = password.Any(ch => !char.IsLetterOrDigit(ch));

            return hasUpperCase && hasDigit && hasSpecialChar;
        }
    }
}