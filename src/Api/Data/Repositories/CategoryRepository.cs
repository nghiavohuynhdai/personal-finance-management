using Api.Common;
using Api.Entities;
using Api.Features.Category;
using Api.Features.Category.GetAllCategory;
using Microsoft.EntityFrameworkCore;
using static Api.Features.Category.CreateCategory.CreateCategoryHandler;
using static Api.Features.Category.GetAllCategory.GetAllCategoryHandler;

namespace Api.Data.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _context;

    public CategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<CreatedCategoryData> CreateCategoryAsync(Category category, CancellationToken cancellationToken)
    {
        var addedResult = await _context.Categories.AddAsync(category);
        return new CreatedCategoryData(addedResult.Entity.Id);
    }

    public async Task<IEnumerable<GetAllCategoryHandler.CategoryData>> GetAllCategoryAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Categories
            .Select(cat => new CategoryData(cat.Type)
            {
                Id = cat.Id,
                Name = cat.Name
            })
        .ToListAsync(cancellationToken);
    }

    public async Task<bool> IsNameAndTypeUniqueAsync(string name, CategoryType type, CancellationToken cancellationToken = default)
    {
        return !await _context.Categories.AnyAsync(cat => cat.Name == name && cat.Type == type, cancellationToken);
    }
}
