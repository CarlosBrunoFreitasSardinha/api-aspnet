using CB.BackDefault.Application.Aggregates.AuthAggregate.Interfaces;
using CB.BackDefault.Application.Aggregates.AuthAggregate.ViewModels;
using CB.BackDefault.Application.Aggregates.AuthAggregate.ViewModels.Response;
using CB.BackDefault.Application.Shared.Settings;
using CB.BackDefault.Domain.Aggregates.AuthAggregate.Interfaces;
using CB.BackDefault.Domain.Aggregates.AuthAggregate.Models;
using CB.BackDefault.Domain.Shared.Interfaces;
using CB.BackDefault.Domain.Shared.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CB.BackDefault.Application.Aggregates.AuthAggregate.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly ITokenService _tokenService;
        private readonly IRefreshTokenService _refreshService;
        private readonly IUnitOfWork _uow;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly JwtSettings _jwtSettings;

        public AuthService(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IOptions<JwtSettings> jwtSettings,
            IRefreshTokenRepository refreshTokenRepository,
            ITokenService tokenService,
            IRefreshTokenService refreshService,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
            _refreshTokenRepository = refreshTokenRepository;
            _tokenService = tokenService;
            _refreshService = refreshService;
            _uow = unitOfWork;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterViewModel model)
        {
            var user = new IdentityUser { UserName = model.Email, Email = model.Email };
            return await _userManager.CreateAsync(user, model.Password);
        }

        public async Task<AuthResponse?> LoginAsync(LoginViewModel model, string ip)
        {
            var result = await _signInManager.PasswordSignInAsync(
                model.Email,
                model.Password,
                false,
                false);

            if (!result.Succeeded)
                return null;

            var user = await _userManager.FindByEmailAsync(model.Email);

            var accessToken = await _tokenService.GenerateAccessToken(user);
            var refreshToken = await _refreshService.CreateTokenAsync(user, ip);

            return new AuthResponse(accessToken, refreshToken, DateTime.Now);
        }

    }
}
