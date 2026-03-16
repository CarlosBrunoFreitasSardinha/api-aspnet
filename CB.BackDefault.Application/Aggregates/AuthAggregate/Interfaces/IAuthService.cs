using CB.BackDefault.Application.Aggregates.AuthAggregate.ViewModels;
using CB.BackDefault.Application.Aggregates.AuthAggregate.ViewModels.Response;
using Microsoft.AspNetCore.Identity;

namespace CB.BackDefault.Application.Aggregates.AuthAggregate.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse?> LoginAsync(LoginViewModel model, string ip);
        Task<IdentityResult> RegisterAsync(RegisterViewModel model);

        // Task<string?> ExternalLoginAsync(ExternalLoginInfo info);
        Task<UserProfileResponse?> GetUserProfileAsync(string userId);
    }
}
