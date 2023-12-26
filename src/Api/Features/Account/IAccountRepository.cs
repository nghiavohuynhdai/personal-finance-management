using static Api.Features.Account.GetAccountDetail.GetAccountDetailHandler;
using static Api.Features.Account.GetAllAccounts.GetAllAccountsHandler;
using static Api.Features.Account.CreateAccount.CreateAccountHandler;
using Api.Common;

namespace Api.Features.Account;

public interface IAccountRepository
{
    Task<IEnumerable<AccountData>> GetAllAccountsAsync(CancellationToken cancellationToken = default);
    Task<AccountDetailData?> GetAccountDetailAsync(Guid id, CancellationToken cancellationToken = default);
    Task<CreatedAccountData> CreateAccountAsync(Entities.Account account, CancellationToken cancellationToken = default);
    Task<bool> IsNameUniqueAsync(string name, CancellationToken cancellationToken = default);
    Task<bool> IsAccountExistAsync(Guid id, CancellationToken cancellationToken = default);
    Task ChangeAccountStatusAsync(Guid id, AccountStatus status, CancellationToken cancellationToken = default);
}