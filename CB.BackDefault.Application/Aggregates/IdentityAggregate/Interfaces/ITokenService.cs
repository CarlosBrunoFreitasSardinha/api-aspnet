using CB.BackDefault.Identity.Models;

namespace CB.BackDefault.Application.Aggregates.IdentityAggregate.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateAccessToken(UserApplication user);
    }
}
