using Api.Common;
using static Api.Features.Category.CreateCategory.CreateCategoryHandler;
using static Api.Features.Category.GetAllCategory.GetAllCategoryHandler;

namespace Api.Features.Category;

public interface ICategoryRepository
{
    Task<IEnumerable<CategoryData>> GetAllCategoryAsync(CancellationToken cancellationToken = default);
    Task<bool> IsNameAndTypeUniqueAsync(string name, CategoryType type, CancellationToken cancellationToken = default);
    Task<CreatedCategoryData> CreateCategoryAsync(Entities.Category category, CancellationToken cancellationToken = default);
}
