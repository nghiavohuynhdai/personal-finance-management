using Api.Features.Account.ChangeAccountStatus;
using Api.Features.Account.CreateAccount;
using Api.Features.Account.GetAccountDetail;
using Api.Features.Account.GetAllAccounts;
using Api.Features.Category.CreateCategory;
using Api.Features.Category.GetAllCategory;
using Api.Features.Transaction.CreateTransaction;

namespace Api.Setup;

public static class ApplicationHandlers
{
    public static IServiceCollection AddHandlers(this IServiceCollection service)
    {
        service.AddTransient<CreateAccountHandler>();
        service.AddTransient<GetAllAccountsHandler>();
        service.AddTransient<GetAccountDetailHandler>();
        service.AddTransient<ChangeAccountStatusHandler>();
        service.AddTransient<CreateCategoryHandler>();
        service.AddTransient<GetAllCategoryHandler>();
        service.AddTransient<CreateTransactionHandler>();
        return service;
    }
}