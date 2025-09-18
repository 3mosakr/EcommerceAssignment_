using Ecommerce.Entities.Models;
using Ecommerce.Entities.Models.Auth;
using Ecommerce.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<string>("Invalid login request"));

            var result = await _authService.AuthenticateAsync(request.UserName, request.Password);

            if (result == null)
                return Unauthorized(new ApiResponse<string>("Invalid username or password"));

            return Ok(new ApiResponse<AuthResponse>(result, "Login successful"));
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] string refreshToken)
        {
            if (string.IsNullOrWhiteSpace(refreshToken))
                return BadRequest(new ApiResponse<string>("Refresh token is required"));

            var result = await _authService.RefreshTokenAsync(refreshToken);

            if (result == null)
                return Unauthorized(new ApiResponse<string>("Invalid or expired refresh token"));

            return Ok(new ApiResponse<AuthResponse>(result, "Token refreshed successfully"));
        }
    }
}
