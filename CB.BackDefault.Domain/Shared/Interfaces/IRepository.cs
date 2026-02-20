using System.Linq.Expressions;
namespace CB.BackDefault.Domain.Shared.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task AdicionarAsync(T entidade);
        Task AdicionarRangeAsync(List<T> entidade);
        void Atualizar(T entidade);
        Task<IEnumerable<T>> ObterTodosAsync(Expression<Func<T, bool>> expressao);
        Task<T> ObterAsync(Expression<Func<T, bool>> expressao);
    }
}
