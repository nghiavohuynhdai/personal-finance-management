using Api.Common;

using Microsoft.AspNetCore.Mvc;

namespace Api.Features.Transaction.CreateTransaction;

public class CreateTransactionEndpoint
{
    public static void Map(RouteGroupBuilder groupBuilder)
    {
        groupBuilder
            .MapPost("", CreateTransaction)
            .WithName(nameof(CreateTransaction))
            .WithOpenApi()
            .Produces<ResultResponse<string>>(StatusCodes.Status201Created)
            .Produces<ResultResponse<object>>(StatusCodes.Status400BadRequest);
    }
    
    private static async Task<IResult> CreateTransaction([FromBody] CreateTransactionRequest request, CreateTransactionHandler handler, CancellationToken cancellationToken)
    {
        var createdTransactionData = await handler.Handle(request, cancellationToken);
        return TypedResults.Json(
            ResultResponse<CreateTransactionHandler.CreatedTransactionData>.Init(createdTransactionData, ""),
            statusCode: StatusCodes.Status201Created
        );
    }
}