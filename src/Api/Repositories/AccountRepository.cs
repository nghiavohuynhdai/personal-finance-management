using Api.Data;
using Api.Entities;
using Api.Features.Account;
using Microsoft.EntityFrameworkCore;
using static Api.Features.Account.GetAllAccounts.GetAllAccountsHandler;
using static Api.Features.Account.CreateAccount.CreateAccountHandler;
using static Api.Features.Account.GetAccountDetail.GetAccountDetailHandler;

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
            .OrderBy(acc => acc.CreatedAt)
            .Select(acc => new AccountData(acc.Status)
            {
                Id = acc.Id,
                Name = acc.Name,
                Balance = acc.Balance
            })
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<AccountDetailData?> GetAccountDetailAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Accounts
        .Select(acc => new AccountDetailData(acc.Status)
        {
            Id = acc.Id,
            Name = acc.Name,
            Balance = acc.Balance,
            TotalLoan = acc.TotalLoan,
            CreatedAt = acc.CreatedAt,
            UpdatedAt = acc.UpdatedAt
        })
        .AsNoTracking()
        .FirstOrDefaultAsync(acc => acc.Id == id, cancellationToken);
    }

    public async Task<CreatedAccountData> CreateAccountAsync(Account account, CancellationToken cancellationToken = default)
    {
        var addedResult = await _context.Accounts.AddAsync(account, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return new CreatedAccountData(addedResult.Entity.Id);
    }

    public async Task<bool> IsNameUniqueAsync(string name, CancellationToken cancellationToken = default)
    {
        return !await _context.Accounts.AnyAsync(acc => acc.Name == name, cancellationToken);
    }
}