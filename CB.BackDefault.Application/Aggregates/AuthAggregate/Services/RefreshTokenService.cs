using CB.BackDefault.Application.Aggregates.AuthAggregate.Interfaces;
using CB.BackDefault.Application.Aggregates.AuthAggregate.ViewModels.Response;
using CB.BackDefault.Domain.Aggregates.AuthAggregate.Interfaces;
using CB.BackDefault.Domain.Aggregates.AuthAggregate.Models;
using CB.BackDefault.Domain.Shared.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System.Text;

namespace CB.BackDefault.Application.Aggregates.AuthAggregate.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly ITokenService _tokenService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUnitOfWork _uow;
        private readonly UserManager<IdentityUser> _userManager;

        public RefreshTokenService(
            UserManager<IdentityUser> userManager,
            ITokenService tokenService,
            IRefreshTokenRepository refreshTokenRepository,
            IUnitOfWork uow)
        {
            _tokenService = tokenService;
            _refreshTokenRepository = refreshTokenRepository;
            _uow = uow;
            _userManager = userManager;
        }

        public async Task<string> CreateTokenAsync(IdentityUser user, string ip)
        {
            var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            var hash = HashToken(token);

            var refreshToken = new RefreshTokenModel(
                                                Guid.NewGuid(), 
                                                hash, 
                                                user.Id, 
                                                DateTime.UtcNow,
                                                DateTime.UtcNow.AddDays(7),
                                                ip);
            try
            {
                await _refreshTokenRepository.AdicionarAsync(refreshToken);
                await _uow.CommitAsync();
            }
            catch (Exception ex) { 
                Console.WriteLine(ex.ToString());
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

        public async Task<(AuthResponse?, string)> RefreshAsync(string refreshToken, string ip)
        {
            var hash = HashToken(refreshToken);

            try
            {
                var token = await _refreshTokenRepository.ObterAsync(x => x.TokenHash == hash);

                if (token == null)
                    return (null, "Token Nulo.");

                if (token.Revoked || token.ExpirationDate < DateTime.UtcNow)
                    return (null, "Refresh token reutilizado. Sessão comprometida.");

                var user = await _userManager.FindByIdAsync(token.UserId);

                token.Revoked = true;
                token.RevokedAt = DateTime.UtcNow;
                token.RevokedByIp = ip;

                var newAccessToken = await _tokenService.GenerateAccessToken(user);
                var newRefreshToken = await CreateTokenAsync(user, ip);
                var newToken = await _refreshTokenRepository.ObterAsync(x => x.TokenHash == HashToken(newRefreshToken));

                token.ReplacedByTokenId = newToken.Id;

                await _uow.CommitAsync();
                return (new AuthResponse(newAccessToken, newRefreshToken, DateTime.Now), "ok");
            }
            catch (Exception ex) { 
                Console.WriteLine(ex);
            }

            return (null, "Falha na Função Refresh token service");
        }
    }
}
