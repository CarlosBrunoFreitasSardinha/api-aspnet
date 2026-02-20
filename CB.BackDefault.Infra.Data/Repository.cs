using CB.BackDefault.Domain.Shared.Interfaces;
using CB.BackDefault.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CB.BackDefault.Infra.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbFactory _dbFactory;
        private DbSet<T> _dbSet;

        protected DbSet<T> Dbset
        {
            get => _dbSet ??= _dbFactory.DbContext.Set<T>();
        }

        public Repository(DbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task AdicionarAsync(T entidade)
        {
            await Dbset.AddAsync(entidade);
        }

        public async Task AdicionarRangeAsync(List<T> entidade)
        {
            await Dbset.AddRangeAsync(entidade);
        }

        public void Atualizar(T entidade)
        {
            Dbset.Update(entidade);
        }

        public async Task<IEnumerable<T>> ObterTodosAsync(Expression<Func<T, bool>> expressao)
        {
            if (expressao != null)
            {
                return await Dbset.Where(expressao).ToListAsync();
            }
            return new List<T>();
        }

        public async Task<T> ObterAsync(Expression<Func<T, bool>> expressao)
        {
            if (expressao != null)
            {
                return await Dbset.FirstAsync(expressao);
            }
            return null;
        }
    }
}
