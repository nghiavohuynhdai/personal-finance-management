using Api.Features.Account;
using Api.Repositories;

namespace Api.Setup;

public static class ApplicationRepositories
{
    public static IServiceCollection AddRepositories(this IServiceCollection service)
    {
        service.AddTransient<IAccountRepository, AccountRepository>();
        return service;
    }
}