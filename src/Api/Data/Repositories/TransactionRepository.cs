using Api.Data.Context;
using Api.Data.Repositories.Interfaces;
using Api.Entities;

namespace Api.Data.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly ApplicationDbContext _context;

    public TransactionRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Guid> CreateTransactionAsync(Transaction transaction, CancellationToken cancellationToken = default)
    {
        var addedResult = await _context.Transactions.AddAsync(transaction, cancellationToken);
        return addedResult.Entity.Id;
    }
}