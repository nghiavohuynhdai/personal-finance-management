using FluentValidation;

namespace Api.Features.Account.CreateAccount;

public class CreateAccountRequestValidator : AbstractValidator<CreateAccountRequest>
{
    public CreateAccountRequestValidator(IAccountRepository repository)
    {
        RuleFor(acc => acc.Name)
            .MustAsync(async (name, cancellationToken) => await repository.IsNameUniqueAsync(name, cancellationToken))
            .WithMessage("Account name is already used");

        RuleFor(acc => acc.Balance)
            .Must(balance => balance >= 0)
            .WithMessage("Balance must be greater than or equal to 0");

        RuleFor(acc => acc.TotalLoan)
            .Must(totalLoan => totalLoan >= 0)
            .WithMessage("Total loan must be greater than or equal to 0");
    }
}