using Api.Common;
using static Api.Features.Category.GetAllCategory.GetAllCategoryHandler;

namespace Api.Data.Repositories.Interfaces;

public interface ICategoryRepository
{
    Task<IEnumerable<CategoryData>> GetAllCategoryAsync(CancellationToken cancellationToken = default);
    IQueryable<Entities.Category> GetCategoryById(Guid id);
    Task<bool> IsNameAndTypeUniqueAsync(string name, CategoryType type, CancellationToken cancellationToken = default);
    Task<Guid> CreateCategoryAsync(Entities.Category category, CancellationToken cancellationToken = default);
}
