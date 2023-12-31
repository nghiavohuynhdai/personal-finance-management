using Api.Common;
using Api.Data.UnitOfWork;
using Api.Exceptions;
using FluentValidation;

namespace Api.Features.Category.CreateCategory;

public class CreateCategoryHandler
{
    private readonly ICategoryRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateCategoryRequest> _validator;

    public CreateCategoryHandler(IUnitOfWork unitOfWork, IValidator<CreateCategoryRequest> validator)
    {
        _repository = unitOfWork.CategoryRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<CreatedCategoryData> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        await ValidateRequest(request, cancellationToken);

        return await CreateCategory(request, cancellationToken);
    }

    private async Task ValidateRequest(CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        var validatedResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validatedResult.IsValid)
        {
            throw new BadRequestException(validatedResult.Errors[0].ErrorMessage);
        }

        var IsNameAndTypeUnique = await _repository.IsNameAndTypeUniqueAsync
        (
            request.Name,
            Enum.Parse<CategoryType>(request.Type),
            cancellationToken
        );

        if (!IsNameAndTypeUnique)
        {
            throw new BadRequestException("Category name is exists");
        }
    }

    private async Task<CreatedCategoryData> CreateCategory(CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        var category = new Entities.Category
        {
            Name = request.Name,
            Type = Enum.Parse<CategoryType>(request.Type)
        };

        var createdCategoryData = await _repository.CreateCategoryAsync(category, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);
        return createdCategoryData;
    }

    public record CreatedCategoryData(Guid Id);
}
