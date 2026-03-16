using CB.BackDefault.Identity.Models;

namespace CB.BackDefault.Application.Aggregates.AuthAggregate.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateAccessToken(UserApplication user);
    }
}
