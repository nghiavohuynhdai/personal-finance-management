using Api.Common;
using static Api.Features.Account.GetAllAccounts.GetAllAccountsHandler;

namespace Api.Features.Account.GetAllAccounts;

public class GetAllAccountsEndpoint
{
    public static void Map(RouteGroupBuilder groupBuilder)
    {
        groupBuilder
            .MapGet("", GetAllAccounts)
            .WithName(nameof(GetAllAccounts))
            .WithOpenApi()
            .Produces<ResultResponse<IEnumerable<AccountData>>>();
    }

    private static async Task<IResult> GetAllAccounts(
        GetAllAccountsHandler handler, CancellationToken cancellationToken)
    {
        var accounts = await handler.Handle(cancellationToken);
        return TypedResults.Json(ResultResponse<IEnumerable<AccountData>>.Init(accounts, ""));
    }
}