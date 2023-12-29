using Api.Common;
using Microsoft.AspNetCore.Mvc;
using static Api.Features.Category.CreateCategory.CreateCategoryHandler;

namespace Api.Features.Category.CreateCategory;

public class CreateCategoryEndpoint
{
    public static void Map(RouteGroupBuilder groupBuilder)
    {
        groupBuilder
            .MapPost("", CreateCategory)
            .WithName(nameof(CreateCategory))
            .WithOpenApi()
            .Produces<ResultResponse<string>>(StatusCodes.Status201Created)
            .Produces<ResultResponse<object>>(StatusCodes.Status400BadRequest);
    }

    private static async Task<IResult> CreateCategory([FromBody] CreateCategoryRequest request, CreateCategoryHandler handler, CancellationToken cancellationToken)
    {
        var createdCategoryData = await handler.Handle(request, cancellationToken);
        return TypedResults.Json(
            ResultResponse<CreatedCategoryData>.Init(createdCategoryData, ""),
            statusCode: StatusCodes.Status201Created
        );
    }
}
