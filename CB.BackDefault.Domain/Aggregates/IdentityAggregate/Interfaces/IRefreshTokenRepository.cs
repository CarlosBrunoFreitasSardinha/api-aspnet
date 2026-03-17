using CB.BackDefault.Domain.Aggregates.IdentityAggregate.Models;
using CB.BackDefault.Domain.Shared.Interfaces;

namespace CB.BackDefault.Domain.Aggregates.IdentityAggregate.Interfaces
{
    public interface IRefreshTokenRepository : IRepository<RefreshTokenModel>
    {
    }
}
