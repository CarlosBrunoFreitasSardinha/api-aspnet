using CB.BackDefault.Domain.Aggregates.AuthAggregate.Interfaces;
using CB.BackDefault.Domain.Aggregates.AuthAggregate.Models;
using CB.BackDefault.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CB.BackDefault.Infra.Data.Aggregates.AuthAggregate.Repositories
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
