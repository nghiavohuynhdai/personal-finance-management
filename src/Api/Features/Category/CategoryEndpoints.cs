using Api.Features.Category.CreateCategory;
using Api.Features.Category.GetAllCategory;

namespace Api.Features.Category;

public static class CategoryEndpoints
{
    public static void Map(RouteGroupBuilder groupBuilder)
    {
        var categoryGroupBuilder = groupBuilder.MapGroup("categories").WithTags("Category");
        CreateCategoryEndpoint.Map(categoryGroupBuilder);
        GetAllCategoryEndpoint.Map(categoryGroupBuilder);
    }
}
