using Api.Features.Account;
using Api.Features.Category;
using Api.Features.Transaction;

namespace Api.Setup;

public static class ApplicationEndpoints
{
    public static void MapEndpoints(this IEndpointRouteBuilder builder)
    {
        var groupBuilder = builder.MapGroup("api");
        AccountEndpoints.Map(groupBuilder);
        CategoryEndpoints.Map(groupBuilder);
        TransactionEndpoints.Map(groupBuilder);
    }
}