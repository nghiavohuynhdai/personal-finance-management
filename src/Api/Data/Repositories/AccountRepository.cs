using Api.Data.Context;
using Api.Entities;
using Microsoft.EntityFrameworkCore;
using static Api.Features.Account.GetAllAccounts.GetAllAccountsHandler;
using static Api.Features.Account.GetAccountDetail.GetAccountDetailHandler;
using Api.Common;
using Api.Data.Repositories.Interfaces;

namespace Api.Data.Repositories;

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
        return await GetAccountById(id)
            .Select(acc => new AccountDetailData(acc.Status)
            {
                Id = acc.Id,
                Name = acc.Name,
                Balance = acc.Balance,
                TotalLoan = acc.TotalLoan,
                CreatedAt = acc.CreatedAt,
                UpdatedAt = acc.UpdatedAt
            })
            .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<Guid> CreateAccountAsync(Account account, CancellationToken cancellationToken = default)
    {
        var addedResult = await _context.Accounts.AddAsync(account, cancellationToken);
        return addedResult.Entity.Id;
    }

    public async Task<bool> IsNameUniqueAsync(string name, CancellationToken cancellationToken = default)
    {
        return !await _context.Accounts.AnyAsync(acc => acc.Name == name, cancellationToken);
    }

    public async Task<bool> IsBalanceEnoughAsync(Guid id, decimal amount, CancellationToken cancellationToken = default)
    {
        return await _context.Accounts
            .AnyAsync(acc => acc.Id == id && acc.Balance >= amount, cancellationToken);
    }

    public IQueryable<Account> GetAccountById(Guid id)
    {
        return _context.Accounts
            .AsNoTracking()
            .Where(acc => acc.Id == id);
    }

    public void ChangeAccountStatus(Account account, AccountStatus status)
    {
        _context.Accounts.Attach(account).Entity.Status = status;
    }

    public void UpdateAccountBalance(Account account, TransactionType transactionType, decimal amount)
    {
        if (transactionType == TransactionType.Expense)
        {
            _context.Accounts.Attach(account).Entity.Balance -= amount;
        }
        else
        {
            _context.Accounts.Attach(account).Entity.Balance += amount;
        }
    }
}