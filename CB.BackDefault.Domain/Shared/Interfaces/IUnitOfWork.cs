namespace CB.BackDefault.Domain.Shared.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
    }
}
