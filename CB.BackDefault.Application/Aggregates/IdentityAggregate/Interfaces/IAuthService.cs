using CB.BackDefault.Application.Aggregates.IdentityAggregate.ViewModels;
using CB.BackDefault.Application.Aggregates.IdentityAggregate.ViewModels.Response;
using Microsoft.AspNetCore.Identity;

namespace CB.BackDefault.Application.Aggregates.IdentityAggregate.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse?> LoginAsync(LoginViewModel model, string ip);
        Task<IdentityResult> RegisterAsync(RegisterViewModel model);

        // Task<string?> ExternalLoginAsync(ExternalLoginInfo info);
        Task<UserProfileResponse?> GetUserProfileAsync(string userId);
        Task<IdentityResult> ChangePasswordAsync(string userId, ChangePasswordViewModel model);
    }
}
