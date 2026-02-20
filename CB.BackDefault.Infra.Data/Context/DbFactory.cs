using Microsoft.EntityFrameworkCore;

namespace CB.BackDefault.Infra.Data.Context
{
    public class DbFactory : IDisposable
    {
        private bool _disposed;
        private Func<BackDefaultContext> _instanceFunc;
        private DbContext _dbContext;

        public DbContext DbContext => _dbContext ??= _instanceFunc.Invoke();

        public DbFactory(Func<BackDefaultContext> dbContextFactory)
        {
            _instanceFunc = dbContextFactory;
        }

        public void Dispose()
        {
            if (!_disposed && _dbContext != null)
            {
                _disposed = true;
                _dbContext.Dispose();
                GC.SuppressFinalize(this);
            }
        }
    }
}
