using Api.Common;
using static Api.Features.Category.CreateCategory.CreateCategoryHandler;

namespace Api.Features.Category;

public interface ICategoryRepository
{
    Task<bool> IsNameAndTypeUniqueAsync(string name, CategoryType type, CancellationToken cancellationToken = default);
    Task<CreatedCategoryData> CreateCategoryAsync(Entities.Category category, CancellationToken cancellationToken = default);
}
