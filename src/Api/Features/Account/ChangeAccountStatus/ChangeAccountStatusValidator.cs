using Api.Common;
using Api.Data.UnitOfWork;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Api.Features.Account.ChangeAccountStatus;

public class ChangeAccountStatusValidator : AbstractValidator<ChangeAccountStatusRequest>
{
    public ChangeAccountStatusValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(request => request.Id)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("Id is required")
            .MustAsync(async (id, cancellationToken) =>
            {
                var account = await unitOfWork.AccountRepository
                    .GetAccountById(id)
                    .SingleOrDefaultAsync(cancellationToken);
                return account is not null;
            })
            .WithMessage("Account is not exist");

        RuleFor(request => request.Status)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .WithMessage("Account status is required")
            .IsEnumName(typeof(AccountStatus))
            .WithMessage("Account status is invalid");
    }
}