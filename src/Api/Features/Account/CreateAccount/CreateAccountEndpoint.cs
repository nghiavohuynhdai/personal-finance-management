using System.Net;
using Api.Common;
using Microsoft.AspNetCore.Mvc;
using static Api.Features.Account.CreateAccount.CreateAccountHandler;

namespace Api.Features.Account.CreateAccount;

public class CreateAccountEndpoint
{
    public static void Map(RouteGroupBuilder groupBuilder)
    {
        groupBuilder
            .MapPost("", CreateAccount)
            .WithName(nameof(CreateAccount))
            .WithOpenApi()
            .Produces<ResultResponse<CreatedAccountData>>(StatusCodes.Status201Created)
            .Produces<ResultResponse<object>>(StatusCodes.Status400BadRequest);
    }

    private static async Task<IResult> CreateAccount(
        [FromBody] CreateAccountRequest request, CreateAccountHandler handler, CancellationToken cancellationToken)
    {
        var createdAccountData = await handler.Handle(request, cancellationToken);
        return TypedResults.Json(
            ResultResponse<CreatedAccountData>.Init(createdAccountData, ""),
            statusCode: (int)HttpStatusCode.Created
        );
    }
}