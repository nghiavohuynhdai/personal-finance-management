using Api.Common;
using Api.Exceptions;
using Microsoft.AspNetCore.Mvc;
using static Api.Features.Account.GetAccountDetail.GetAccountDetailHandler;

namespace Api.Features.Account.GetAccountDetail;

public class GetAccountDetailEndpoint
{
    public static void Map(RouteGroupBuilder groupBuilder)
    {
        groupBuilder
        .MapGet("{id}", GetAccountDetail)
        .WithName(nameof(GetAccountDetail))
        .WithOpenApi()
        .Produces<ResultResponse<AccountDetailData>>()
        .Produces<ResultResponse<object>>(StatusCodes.Status404NotFound);
    }

    private static async Task<IResult> GetAccountDetail(
        [FromRoute] Guid id, GetAccountDetailHandler handler, CancellationToken cancellationToken)
    {
        var account = await handler.Handle(id, cancellationToken);

        if (account is null)
        {
            throw new NotFoundException($"Account with id {id} not found");
        }

        return TypedResults.Json(ResultResponse<AccountDetailData>.Init(account, ""));
    }
}
