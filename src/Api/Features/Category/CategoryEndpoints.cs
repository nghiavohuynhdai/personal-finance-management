using Api.Features.Category.CreateCategory;

namespace Api.Features.Category;

public static class CategoryEndpoints
{
    public static void Map(RouteGroupBuilder groupBuilder)
    {
        var categoryGroupBuilder = groupBuilder.MapGroup("categories").WithTags("Category");
        CreateCategoryEndpoint.Map(categoryGroupBuilder);
    }
}
