using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Silvernet.Services;

namespace Silvernet.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IJwtService _jwtService;

        public AuthController(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(new { message = "Email and password are required." });
            }

            // Hardcoded credentials
            if (request.Email != "ido@example.com" || request.Password != "123456")
            {
                return Unauthorized(new { message = "Invalid credentials." });
            }

            var token = _jwtService.GenerateToken(1, request.Email);

            return Ok(new 
            { 
                token,
                userId = 1,
                email = request.Email,
                expiresIn = 3600 
            });
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}