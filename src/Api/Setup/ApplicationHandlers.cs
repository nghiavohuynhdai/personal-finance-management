using Api.Features.Account.CreateAccount;

namespace Api.Setup;

public static class ApplicationHandlers
{
    public static IServiceCollection AddHandlers(this IServiceCollection service)
    {
        service.AddTransient<CreateAccountHandler>();
        return service;
    }
}