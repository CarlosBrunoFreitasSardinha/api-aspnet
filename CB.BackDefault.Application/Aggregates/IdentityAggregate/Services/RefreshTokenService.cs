using CB.BackDefault.Application.Aggregates.IdentityAggregate.Interfaces;
using CB.BackDefault.Application.Aggregates.IdentityAggregate.ViewModels.Response;
using CB.BackDefault.Domain.Aggregates.IdentityAggregate.Models;
using CB.BackDefault.Domain.Aggregates.IdentityAggregate.Interfaces;
using CB.BackDefault.Domain.Exceptions;
using CB.BackDefault.Domain.Shared.Interfaces;
using CB.BackDefault.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System.Text;

namespace CB.BackDefault.Application.Aggregates.IdentityAggregate.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly ITokenService _tokenService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUnitOfWork _uow;
        private readonly UserManager<UserApplication> _userManager;

        public RefreshTokenService(
                ITokenService tokenService,
                IRefreshTokenRepository refreshTokenRepository,
                IUnitOfWork uow,
                UserManager<UserApplication> userManager)
        {
            _tokenService = tokenService;
            _refreshTokenRepository = refreshTokenRepository;
            _uow = uow;
            _userManager = userManager;
        }

        public async Task<string> CreateTokenAsync(UserApplication user, string ip)
        {
            var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            var hash = HashToken(token);

            var refreshToken = new RefreshTokenModel(Guid.NewGuid(), hash, user.Id, DateTime.UtcNow, DateTime.UtcNow.AddDays(7), ip);

            try
            {
                await _refreshTokenRepository.AdicionarAsync(refreshToken);
                await _uow.CommitAsync();
            }
            catch (DomainException)
            {
                throw new UnauthorizedDomainException("Não foi possível Criar Token.");
            }

            return token;
        }

        private string HashToken(string token)
        {
            using var sha = SHA256.Create();

            var bytes = Encoding.UTF8.GetBytes(token);
            var hash = sha.ComputeHash(bytes);

            return Convert.ToBase64String(hash);
        }

        public async Task RevokeAllTokensAsync(string userId)
        {

            var tokens = await _refreshTokenRepository.ObterTodosAsync(x => x.UserId == userId && !x.Revoked);

            foreach (var token in tokens)
            {
                token.Revoked = true;
                token.RevokedAt = DateTime.UtcNow;
            }
            await _uow.CommitAsync();
        }

        public async Task<AuthResponse?> RefreshAsync(string refreshToken, string ip)
        {
            var hash = HashToken(refreshToken);


            var token = await _refreshTokenRepository.ObterAsync(x => x.TokenHash == hash);

            if (token == null)
                throw new UnauthorizedDomainException("Refresh token inválido.");

            if (token.Revoked || token.ExpirationDate < DateTime.UtcNow)
                throw new UnauthorizedDomainException("Refresh token reutilizado. Sessão comprometida.");

            var user = await _userManager.FindByIdAsync(token.UserId);

            if (user == null)
                throw new NotFoundDomainException("Usuário", token.UserId);

            token.Revoked = true;
            token.RevokedAt = DateTime.UtcNow;
            token.RevokedByIp = ip;

            var newAccessToken = await _tokenService.GenerateAccessToken(user);
            var newRefreshToken = await CreateTokenAsync(user, ip);
            var newToken = await _refreshTokenRepository.ObterAsync(x => x.TokenHash == HashToken(newRefreshToken));

            token.ReplacedByTokenId = newToken.Id;

            await _uow.CommitAsync();

            return new AuthResponse(newAccessToken, newRefreshToken, DateTime.Now);


        }
    }
}
