using Api.Data.UnitOfWork;
using Api.Exceptions;
using FluentValidation;

namespace Api.Features.Account.CreateAccount;

public class CreateAccountHandler
{
    private readonly IAccountRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateAccountRequest> _validator;

    public CreateAccountHandler(IUnitOfWork unitOfWork, IValidator<CreateAccountRequest> validator)
    {
        _repository = unitOfWork.AccountRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<CreatedAccountData> Handle(CreateAccountRequest request, CancellationToken cancellationToken)
    {
        await ValidateRequest(request, cancellationToken);

        return await CreateAccount(request, cancellationToken);
    }

    private async Task ValidateRequest(CreateAccountRequest request, CancellationToken cancellationToken)
    {
        var validatedResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validatedResult.IsValid)
        {
            throw new BadRequestException(validatedResult.Errors[0].ErrorMessage);
        }
    }

    private async Task<CreatedAccountData> CreateAccount(CreateAccountRequest request, CancellationToken cancellationToken)
    {
        var account = new Entities.Account
        {
            Name = request.Name,
            Balance = request.Balance,
            TotalLoan = request.TotalLoan
        };

        var createdAccountData = await _repository.CreateAccountAsync(account, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);
        return createdAccountData;
    }

    public record CreatedAccountData(Guid Id);
}