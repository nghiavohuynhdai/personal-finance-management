using Api.Common;
using FluentValidation;

namespace Api.Features.Category.CreateCategory;

public class CreateCategoryValidator : AbstractValidator<CreateCategoryRequest>
{
    public CreateCategoryValidator()
    {
        RuleFor(cat => cat.Type)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .WithMessage("Category type is required")
            .IsEnumName(typeof(CategoryType))
            .WithMessage("Category type is invalid");

        RuleFor(cat => cat.Name)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .WithMessage("Category name is required")
            .Length(3, 30)
            .WithMessage("Category name must be between 3 and 30 characters");
    }
}
