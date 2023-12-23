using Api.Features.Account.CreateAccount;

namespace Api.Features.Account;

public static class AccountEndpoints
{
    public static void Map(RouteGroupBuilder groupBuilder)
    {
        var accountGroupBuilder = groupBuilder.MapGroup("accounts").WithTags("Account");
        CreateAccountEndpoint.Map(accountGroupBuilder);
    }
}