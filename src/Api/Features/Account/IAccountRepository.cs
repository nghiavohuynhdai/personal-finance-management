using AccountData = Api.Features.Account.GetAllAccounts.GetAllAccountsHandler.AccountData;

namespace Api.Features.Account;

public interface IAccountRepository
{
    Task<IEnumerable<AccountData>> GetAllAccountsAsync(CancellationToken cancellationToken = default);
    Task<Guid> CreateAccountAsync(Entities.Account account, CancellationToken cancellationToken = default);
    Task<bool> IsNameUniqueAsync(string name, CancellationToken cancellationToken = default);
}