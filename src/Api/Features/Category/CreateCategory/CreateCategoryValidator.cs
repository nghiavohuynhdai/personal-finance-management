using Api.Common;
using Api.Data.UnitOfWork;
using FluentValidation;

namespace Api.Features.Category.CreateCategory;

public class CreateCategoryValidator : AbstractValidator<CreateCategoryRequest>
{
    public CreateCategoryValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(cat => cat.Type)
            .NotNull()
            .WithMessage("Category type is required")
            .IsEnumName(typeof(CategoryType))
            .WithMessage("Category type is invalid");

        RuleFor(cat => cat.Name)
            .NotNull()
            .WithMessage("Category name is required")
            .Length(3, 30)
            .WithMessage("Category name must be between 3 and 30 characters");
    }
}
