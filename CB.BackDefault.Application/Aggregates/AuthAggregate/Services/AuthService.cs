using CB.BackDefault.Application.Aggregates.AuthAggregate.Interfaces;
using CB.BackDefault.Application.Aggregates.AuthAggregate.ViewModels;
using CB.BackDefault.Application.Aggregates.AuthAggregate.ViewModels.Response;
using Microsoft.AspNetCore.Identity;

namespace CB.BackDefault.Application.Aggregates.AuthAggregate.Services
{
    public class AuthService : IAuthService
    {
        private readonly ITokenService _tokenService;
        private readonly IRefreshTokenService _refreshService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthService(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ITokenService tokenService,
            IRefreshTokenService refreshService
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _refreshService = refreshService;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterViewModel model)
        {
            var user = new IdentityUser 
            { 
                UserName = model.Email, 
                Email = model.Email
            };

            return await _userManager.CreateAsync(user, model.Password);
        }

        public async Task<AuthResponse?> LoginAsync(LoginViewModel model, string ip)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (!result.Succeeded)
                return null;

            var user = await _userManager.FindByEmailAsync(model.Email);

            var accessToken = await _tokenService.GenerateAccessToken(user);
            var refreshToken = await _refreshService.CreateTokenAsync(user, ip);

            return new AuthResponse(accessToken, refreshToken, DateTime.Now);
        }
    }
}