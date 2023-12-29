using Api.Common;
using Api.Entities;
using Api.Features.Category;
using Microsoft.EntityFrameworkCore;
using static Api.Features.Category.CreateCategory.CreateCategoryHandler;

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

    public async Task<bool> IsNameAndTypeUniqueAsync(string name, CategoryType type, CancellationToken cancellationToken = default)
    {
       return !await _context.Categories.AnyAsync(cat => cat.Name == name && cat.Type == type, cancellationToken);
    }
}
