using CB.BackDefault.Domain.Shared.Interfaces;
using CB.BackDefault.Infra.Data.Context;

namespace CB.BackDefault.Infra.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbFactory _dbFactory;

        public UnitOfWork(DbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public Task<int> CommitAsync()
        {
            return _dbFactory.DbContext.SaveChangesAsync();
        }
    }
}
