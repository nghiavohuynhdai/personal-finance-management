using Api.Features.Account;

namespace Api.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        IAccountRepository AccountRepository { get; }
        void Commit();
        void Rollback();
        Task CommitAsync(CancellationToken cancellationToken = default);
        Task RollbackAsync(CancellationToken cancellationToken = default);
    }
}