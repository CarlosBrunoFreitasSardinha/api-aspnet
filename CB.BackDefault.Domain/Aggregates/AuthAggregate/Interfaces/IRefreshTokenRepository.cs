using CB.BackDefault.Domain.Aggregates.AuthAggregate.Models;
using CB.BackDefault.Domain.Shared.Interfaces;

namespace CB.BackDefault.Domain.Aggregates.AuthAggregate.Interfaces
{
    public interface IRefreshTokenRepository : IRepository<RefreshTokenModel>
    {
    }
}
