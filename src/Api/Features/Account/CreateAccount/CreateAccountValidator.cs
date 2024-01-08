using Api.Data.UnitOfWork;
using FluentValidation;

namespace Api.Features.Account.CreateAccount;

public class CreateAccountValidator : AbstractValidator<CreateAccountRequest>
{
    public CreateAccountValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(acc => acc.Name)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .WithMessage("Account name is required")
            .Length(3, 100)
            .WithMessage("Account name must be between 3 and 100 characters")
            .MustAsync(async (name, cancellationToken) => await unitOfWork.AccountRepository.IsNameUniqueAsync(name, cancellationToken))
            .WithMessage("Account is exists");

        RuleFor(acc => acc.Balance)
            .Cascade(CascadeMode.Stop)
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(1000000000000)
            .WithMessage("Balance must be greater than or equal to 0");

        RuleFor(acc => acc.TotalLoan)
            .Cascade(CascadeMode.Stop)
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(1000000000000)
            .WithMessage("Total loan must be greater than or equal to 0");
    }
}