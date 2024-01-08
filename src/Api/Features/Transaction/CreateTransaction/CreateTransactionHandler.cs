using System.Globalization;
using Api.Common;
using Api.Data.Repositories.Interfaces;
using Api.Data.UnitOfWork;
using Api.Exceptions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Api.Features.Transaction.CreateTransaction;

public class CreateTransactionHandler
{
    private readonly IValidator<CreateTransactionRequest> _validator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITransactionRepository _repository;

    public CreateTransactionHandler(IUnitOfWork unitOfWork, IValidator<CreateTransactionRequest> validator)
    {
        _unitOfWork = unitOfWork;
        _repository = unitOfWork.TransactionRepository;
        _validator = validator;
    }

    public async Task<CreatedTransactionData> Handle(CreateTransactionRequest request,
        CancellationToken cancellationToken)
    {
        await ValidateRequest(request, cancellationToken);
        return await CreateTransaction(request, cancellationToken);
    }

    private async Task ValidateRequest(CreateTransactionRequest request, CancellationToken cancellationToken)
    {
        var validatedResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validatedResult.IsValid)
        {
            throw new BadRequestException(validatedResult.Errors[0].ErrorMessage);
        }

        if (
            request.Type == Enum.GetName(TransactionType.Expense) &&
            !await _unitOfWork.AccountRepository.IsBalanceEnoughAsync(request.AccountId, request.Amount,
                cancellationToken)
        )
        {
            throw new BadRequestException("Balance is not enough");
        }
    }

    private async Task<CreatedTransactionData> CreateTransaction(CreateTransactionRequest request,
        CancellationToken cancellationToken)
    {
        var transaction = new Entities.Transaction
        {
            AccountId = request.AccountId,
            CategoryId = request.CategoryId,
            Type = Enum.Parse<TransactionType>(request.Type),
            Amount = request.Amount,
            Time = DateTime.ParseExact(request.Time, "dd/MM/yyyy HH:mm zz", CultureInfo.InvariantCulture, DateTimeStyles.None).ToUniversalTime(),
            Description = request.Description
        };

        var id = await _repository.CreateTransactionAsync(transaction, cancellationToken);

        var account = await _unitOfWork.AccountRepository
            .GetAccountById(request.AccountId)
            .SingleOrDefaultAsync(cancellationToken);
        _unitOfWork.AccountRepository.UpdateAccountBalance(account, transaction.Type, transaction.Amount);

        await _unitOfWork.CommitAsync(cancellationToken);

        return new CreatedTransactionData(id);
    }

    public record CreatedTransactionData(Guid Id);
}