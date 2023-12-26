using Api.Common;
using FluentValidation;

namespace Api.Features.Account.ChangeAccountStatus;

public class ChangeAccountStatusValidator : AbstractValidator<ChangeAccountStatusRequest>
{
    public ChangeAccountStatusValidator()
    {
        RuleFor(request => request.Id)
            .NotNull()
            .NotEmpty()
            .WithMessage("Id is required");

        RuleFor(request => request.Status)
            .IsEnumName(typeof(AccountStatus))
            .WithMessage("Status is invalid");
    }
}
