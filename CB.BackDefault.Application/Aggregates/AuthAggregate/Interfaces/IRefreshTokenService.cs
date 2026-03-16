using CB.BackDefault.Application.Aggregates.AuthAggregate.ViewModels.Response;
using CB.BackDefault.Identity.Models;

namespace CB.BackDefault.Application.Aggregates.AuthAggregate.Interfaces
{
    public interface IRefreshTokenService
    {
        Task<string> CreateTokenAsync(UserApplication user, string ipAddress);
        Task<AuthResponse?> RefreshAsync(string refreshToken, string ipAddress);
        Task RevokeAllTokensAsync(string userId);
    }
}
