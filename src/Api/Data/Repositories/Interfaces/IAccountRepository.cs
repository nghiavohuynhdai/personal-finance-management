using Api.Common;
using Api.Entities;
using static Api.Features.Account.GetAccountDetail.GetAccountDetailHandler;
using static Api.Features.Account.GetAllAccounts.GetAllAccountsHandler;

namespace Api.Data.Repositories.Interfaces;

public interface IAccountRepository
{
    Task<IEnumerable<AccountData>> GetAllAccountsAsync(CancellationToken cancellationToken = default);
    Task<AccountDetailData?> GetAccountDetailAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Guid> CreateAccountAsync(Account account, CancellationToken cancellationToken = default);
    Task<bool> IsNameUniqueAsync(string name, CancellationToken cancellationToken = default);
    Task<bool> IsBalanceEnoughAsync(Guid id, decimal amount, CancellationToken cancellationToken = default);
    IQueryable<Account> GetAccountById(Guid id);
    void ChangeAccountStatus(Account account, AccountStatus status);
    void UpdateAccountBalance(Account account, TransactionType transactionType, decimal amount);
}