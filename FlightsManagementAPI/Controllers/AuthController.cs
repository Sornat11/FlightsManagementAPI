using FlightsManagementAPI.Models;
using FlightsManagementAPI.Services.AuthService;
using Microsoft.AspNetCore.Mvc;



namespace FlightsManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }

            var user = await _authService.Register(request);
            if (user == null)
            {
                return BadRequest("Registration failed.");
            }

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var token = await _authService.Login(request);
            if (token == null)
            {
                return BadRequest("Login failed.");
            }
            return Ok(token);
        }
    }
}