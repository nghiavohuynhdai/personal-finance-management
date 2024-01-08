using Api.Common;
using Api.Data.Context;
using Api.Data.Repositories.Interfaces;
using Api.Entities;
using Microsoft.EntityFrameworkCore;
using static Api.Features.Category.GetAllCategory.GetAllCategoryHandler;

namespace Api.Data.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _context;

    public CategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Guid> CreateCategoryAsync(Category category, CancellationToken cancellationToken)
    {
        var addedResult = await _context.Categories.AddAsync(category);
        return addedResult.Entity.Id;
    }

    public async Task<IEnumerable<CategoryData>> GetAllCategoryAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Categories
            .Select(cat => new CategoryData(cat.Type)
            {
                Id = cat.Id,
                Name = cat.Name
            })
        .ToListAsync(cancellationToken);
    }

    public IQueryable<Category> GetCategoryById(Guid id)
    {
        return _context.Categories
            .AsNoTracking()
            .Where(cat => cat.Id == id);
    }

    public async Task<bool> IsNameAndTypeUniqueAsync(string name, CategoryType type, CancellationToken cancellationToken = default)
    {
        return !await _context.Categories.AnyAsync(cat => cat.Name == name && cat.Type == type, cancellationToken);
    }
}
