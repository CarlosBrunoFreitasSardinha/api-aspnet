using CB.BackDefault.Application.Aggregates.AuthAggregate.ViewModels.Response;
using Microsoft.AspNetCore.Identity;

namespace CB.BackDefault.Application.Aggregates.AuthAggregate.Interfaces
{
    public interface IRefreshTokenService
    {
        Task<string> CreateTokenAsync(IdentityUser user, string ipAddress);
        Task<AuthResponse?> RefreshAsync(string refreshToken, string ipAddress);
        Task RevokeAllTokensAsync(string userId);
    }
}
