using CB.BackDefault.Application.Aggregates.AuthAggregate.Interfaces;
using CB.BackDefault.Application.Aggregates.AuthAggregate.ViewModels;
using CB.BackDefault.Application.Aggregates.AuthAggregate.ViewModels.Response;
using CB.BackDefault.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace CB.BackDefault.Application.Aggregates.AuthAggregate.Services
{
    public class AuthService : IAuthService
    {
        private readonly ITokenService _tokenService;
        private readonly IRefreshTokenService _refreshService;
        private readonly UserManager<UserApplication> _userManager;
        private readonly SignInManager<UserApplication> _signInManager;

        public AuthService(
            UserManager<UserApplication> userManager,
            SignInManager<UserApplication> signInManager,
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
            var user = new UserApplication 
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

        public async Task<UserProfileResponse?> GetUserProfileAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return null;

            return new UserProfileResponse
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                LastName = user.LastName,
                UrlProfile = user.UrlProfile
            };
        }
        public async Task<IdentityResult> ChangePasswordAsync(string userId, ChangePasswordViewModel model)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "Usuário não encontrado" });

            return await _userManager.ChangePasswordAsync(
                user,
                model.CurrentPassword,
                model.NewPassword
            );
        }
    }
}