using System.Net;
using Api.Common;
using Microsoft.AspNetCore.Mvc;

namespace Api.Features.Account.CreateAccount;

public class CreateAccountEndpoint
{
    public static void Map(RouteGroupBuilder groupBuilder)
    {
        groupBuilder
            .MapPost("", CreateAccount)
            .WithName(nameof(CreateAccount))
            .WithOpenApi()
            .Produces<ResultResponse<CreatedAccountData>>(StatusCodes.Status201Created);
    }

    private static async Task<IResult> CreateAccount(
        [FromBody] CreateAccountRequest request, CreateAccountHandler handler)
    {
        var accountId = await handler.Handle(request, CancellationToken.None);
        var response = ResultResponse<CreatedAccountData>.Init(new CreatedAccountData(accountId), "");
        return TypedResults.Json(response, statusCode: (int)HttpStatusCode.Created);
    }

    private record CreatedAccountData(Guid Id);
}