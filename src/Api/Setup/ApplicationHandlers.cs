using Api.Features.Account.CreateAccount;
using Api.Features.Account.GetAllAccounts;

namespace Api.Setup;

public static class ApplicationHandlers
{
    public static IServiceCollection AddHandlers(this IServiceCollection service)
    {
        service.AddTransient<CreateAccountHandler>();
        service.AddTransient<GetAllAccountsHandler>();
        return service;
    }
}