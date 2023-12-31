using Api.Common;
using static Api.Features.Category.GetAllCategory.GetAllCategoryHandler;

namespace Api.Features.Category.GetAllCategory;

public class GetAllCategoryEndpoint
{
    public static void Map(RouteGroupBuilder groupBuilder)
    {
        groupBuilder
            .MapGet("", GetAllCategory)
            .WithName(nameof(GetAllCategory))
            .WithOpenApi()
            .Produces<ResultResponse<IEnumerable<CategoryData>>>();
    }

    private static async Task<IResult> GetAllCategory(
        GetAllCategoryHandler handler, CancellationToken cancellationToken)
    {
        var categories = await handler.Handle(cancellationToken);
        return TypedResults.Json(ResultResponse<IEnumerable<CategoryData>>.Init(categories, ""));
    }
}
