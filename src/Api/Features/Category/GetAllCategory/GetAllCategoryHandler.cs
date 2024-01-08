using Api.Common;
using Api.Data.Repositories.Interfaces;
using Api.Data.UnitOfWork;

namespace Api.Features.Category.GetAllCategory;

public class GetAllCategoryHandler
{
    private readonly ICategoryRepository _repository;

    public GetAllCategoryHandler(IUnitOfWork unitOfWork)
    {
        _repository = unitOfWork.CategoryRepository;
    }

    public async Task<IEnumerable<CategoryData>> Handle(CancellationToken cancellationToken)
    {
        var categories = await _repository.GetAllCategoryAsync(cancellationToken);
        return categories;
    }

    public record CategoryData
    {
        private readonly CategoryType _type;

        public CategoryData(CategoryType type)
        {
            _type = type;
        }

        public Guid Id { get; init; }
        public string Name { get; set; }
        public string Type => Enum.GetName(_type);
    }
}
