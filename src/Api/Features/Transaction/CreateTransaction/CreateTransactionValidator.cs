using System.Globalization;
using Api.Common;
using Api.Data.UnitOfWork;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Api.Features.Transaction.CreateTransaction;

public class CreateTransactionValidator : AbstractValidator<CreateTransactionRequest>
{
    public CreateTransactionValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(trans => trans.Type)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .WithMessage("Transaction type is required")
            .Must(type =>
                type == Enum.GetName(TransactionType.Income) ||
                type == Enum.GetName(TransactionType.Expense))
            .WithMessage("Transaction type is invalid");

        RuleFor(trans => trans.Description)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .WithMessage("Transaction description is required")
            .Length(0, 500)
            .WithMessage("Transaction description must be less than or equal to 500 characters");

        RuleFor(trans => trans.Amount)
            .Cascade(CascadeMode.Stop)
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(1000000000000)
            .WithMessage("Transaction amount must be greater than or equal to 0");

        RuleFor(trans => trans.Time)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .WithMessage("Transaction time is required")
            .Must((time) =>
            {
                var format = "dd/MM/yyyy HH:mm zz";
                return DateTime.TryParseExact(time, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
            })
            .WithMessage("Transaction time must have format dd/MM/yyyy HH:mm zz");

        RuleFor(trans => trans.AccountId)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .WithMessage("Transaction account id is required")
            .MustAsync(async (accountId, cancellationToken) =>
            {
                var account = await unitOfWork.AccountRepository
                    .GetAccountById(accountId)
                    .SingleOrDefaultAsync(cancellationToken);
                return account is not null;
            })
            .WithMessage("Transaction account id is not exist");

        RuleFor(trans => trans.CategoryId)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .WithMessage("Transaction category id is required")
            .MustAsync(async (categoryId, cancellationToken) =>
            {
                var category = await unitOfWork.CategoryRepository
                    .GetCategoryById(categoryId)
                    .SingleOrDefaultAsync(cancellationToken);
                return category is not null;
            })
            .WithMessage("Transaction category id is not exist");
    }
}