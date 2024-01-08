namespace Api.Data.Repositories.Interfaces;

public interface ITransactionRepository
{
    Task<Guid> CreateTransactionAsync(Entities.Transaction transaction, CancellationToken cancellationToken = default);
}