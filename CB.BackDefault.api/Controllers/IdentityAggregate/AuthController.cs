using CB.BackDefault.Application.Aggregates.IdentityAggregate.Interfaces;
using CB.BackDefault.Application.Aggregates.IdentityAggregate.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CB.BackDefault.Api.Controllers.IdentityAggregate
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IRefreshTokenService _refreshTokenService;

        public AuthController(
                IAuthService authService, 
                IRefreshTokenService refreshTokenService)
        {
            _authService = authService;
            _refreshTokenService = refreshTokenService;
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {

            if (string.IsNullOrEmpty(model?.Email) || string.IsNullOrEmpty(model?.Password) || string.IsNullOrEmpty(model?.ConfirmPassword))
                return BadRequest();

            var result = await _authService.RegisterAsync(model);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok();
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            if (string.IsNullOrEmpty(model?.Email) || string.IsNullOrEmpty(model?.Password))
                return BadRequest();

            var ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "";

            var result = await _authService.LoginAsync(model, ip);

            if (result == null)
                return Unauthorized("Usuário ou Senha Incorretos");

            return Ok(result);
        }

        [Authorize]
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
                return BadRequest();

            var ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "";

            var newAccessToken= await _refreshTokenService.RefreshAsync(refreshToken, ip);

            if (newAccessToken == null)
                return Unauthorized();

            return Ok(newAccessToken);
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var user = await _authService.GetUserProfileAsync(userId);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {

            if (string.IsNullOrEmpty(model?.CurrentPassword) || string.IsNullOrEmpty(model?.NewPassword))
                return BadRequest();

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var result = await _authService.ChangePasswordAsync(userId, model);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("Senha alterada com sucesso");
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";

            await _refreshTokenService.RevokeAllTokensAsync(userId);

            return Ok();
        }
    }
}