using Api.Features.Account;
using Api.Features.Category;

namespace Api.Setup;

public static class ApplicationEndpoints
{
    public static void MapEndpoints(this IEndpointRouteBuilder builder)
    {
        var groupBuilder = builder.MapGroup("api");
        AccountEndpoints.Map(groupBuilder);
        CategoryEndpoints.Map(groupBuilder);
    }
}