using AccountData = Api.Features.Account.GetAllAccounts.GetAllAccountsHandler.AccountData;
using CreatedAccountData = Api.Features.Account.CreateAccount.CreateAccountHandler.CreatedAccountData;

namespace Api.Features.Account;

public interface IAccountRepository
{
    Task<IEnumerable<AccountData>> GetAllAccountsAsync(CancellationToken cancellationToken = default);
    Task<CreatedAccountData> CreateAccountAsync(Entities.Account account, CancellationToken cancellationToken = default);
    Task<bool> IsNameUniqueAsync(string name, CancellationToken cancellationToken = default);
}