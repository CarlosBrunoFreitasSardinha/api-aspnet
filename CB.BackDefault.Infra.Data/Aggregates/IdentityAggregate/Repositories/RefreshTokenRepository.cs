using CB.BackDefault.Domain.Aggregates.IdentityAggregate.Interfaces;
using CB.BackDefault.Domain.Aggregates.IdentityAggregate.Models;
using CB.BackDefault.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CB.BackDefault.Infra.Data.Aggregates.IdentityAggregate.Repositories
{
    public class RefreshTokenRepository : Repository<RefreshTokenModel>, IRefreshTokenRepository
    {
        private readonly DbSet<RefreshTokenModel> _dbSet;

        public RefreshTokenRepository(DbFactory dbFactory) : base(dbFactory)
        {
            _dbSet = dbFactory.DbContext.Set<RefreshTokenModel>();
        }
    }
}
