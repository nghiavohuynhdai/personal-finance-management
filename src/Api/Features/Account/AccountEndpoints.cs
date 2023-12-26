using Api.Features.Account.CreateAccount;
using Api.Features.Account.GetAccountDetail;
using Api.Features.Account.GetAllAccounts;

namespace Api.Features.Account;

public static class AccountEndpoints
{
    public static void Map(RouteGroupBuilder groupBuilder)
    {
        var accountGroupBuilder = groupBuilder.MapGroup("accounts").WithTags("Account");
        CreateAccountEndpoint.Map(accountGroupBuilder);
        GetAllAccountsEndpoint.Map(accountGroupBuilder);
        GetAccountDetailEndpoint.Map(accountGroupBuilder);
    }
}