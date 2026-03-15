using Microsoft.AspNetCore.Identity;

namespace CB.BackDefault.Application.Aggregates.AuthAggregate.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateAccessToken(IdentityUser user);
    }
}
