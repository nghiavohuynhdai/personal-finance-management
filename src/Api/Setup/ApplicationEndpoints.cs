using Api.Features.Account;

namespace Api.Setup;

public static class ApplicationEndpoints
{
    public static void MapEndpoints(this IEndpointRouteBuilder builder)
    {
        var groupBuilder = builder.MapGroup("api");
        AccountEndpoints.Map(groupBuilder);
    }
}