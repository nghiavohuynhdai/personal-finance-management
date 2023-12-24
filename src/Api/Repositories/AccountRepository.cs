using Api.Data;
using Api.Entities;
using Api.Features.Account;
using Api.Features.Account.GetAllAccounts;
using AccountData = Api.Features.Account.GetAllAccounts.GetAllAccountsHandler.AccountData;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly ApplicationDbContext _context;

    public AccountRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AccountData>> GetAllAccountsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Accounts
            .Where(acc => acc.IsDeleted == false)
            .OrderBy(acc => acc.CreatedAt)
            .Select(acc => new GetAllAccountsHandler.AccountData(acc.Status)
            {
                Id = acc.Id,
                Name = acc.Name,
                Balance = acc.Balance
            })
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Guid> CreateAccountAsync(Account account, CancellationToken cancellationToken = default)
    {
        var addedResult = await _context.Accounts.AddAsync(account, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return addedResult.Entity.Id;
    }

    public async Task<bool> IsNameUniqueAsync(string name, CancellationToken cancellationToken = default)
    {
        return !(await _context.Accounts.AnyAsync(acc => acc.Name == name, cancellationToken));
    }
}