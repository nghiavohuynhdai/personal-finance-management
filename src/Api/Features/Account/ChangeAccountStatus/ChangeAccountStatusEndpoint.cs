using Api.Common;
using Microsoft.AspNetCore.Mvc;
using static Api.Features.Account.ChangeAccountStatus.ChangeAccountStatusHandler;

namespace Api.Features.Account.ChangeAccountStatus;

public class ChangeAccountStatusEndpoint
{
    public static void Map(RouteGroupBuilder groupBuilder)
    {
        groupBuilder
            .MapPut("status", ChangeAccountStatus)
            .WithName(nameof(ChangeAccountStatus))
            .WithOpenApi()
            .Produces<ResultResponse<ChangedAccountStatusData>>()
            .Produces<ResultResponse<object>>(StatusCodes.Status400BadRequest);
    }

    private static async Task<IResult> ChangeAccountStatus([FromBody] ChangeAccountStatusRequest request, ChangeAccountStatusHandler handler, CancellationToken cancellationToken)
    {
        var changedAccountStatusData = await handler.Handle(request, cancellationToken);

        return TypedResults.Json(ResultResponse<ChangedAccountStatusData>.Init(changedAccountStatusData, ""));
    }
}
