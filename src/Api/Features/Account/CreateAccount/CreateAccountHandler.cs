using Api.Exceptions;
using FluentValidation;

namespace Api.Features.Account.CreateAccount;

public class CreateAccountHandler
{
    private readonly IAccountRepository _repository;
    private readonly IValidator<CreateAccountRequest> _validator;

    public CreateAccountHandler(IAccountRepository repository, IValidator<CreateAccountRequest> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task<CreatedAccountData> Handle(CreateAccountRequest request, CancellationToken cancellationToken)
    {
        var validatedResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validatedResult.IsValid)
        {
            throw new BadRequestException(validatedResult.Errors[0].ErrorMessage);
        }

        var account = new Entities.Account
        {
            Name = request.Name,
            Balance = request.Balance,
            TotalLoan = request.TotalLoan
        };

        var createdAccountData = await _repository.CreateAccountAsync(account, cancellationToken);
        return createdAccountData;
    }

    public record CreatedAccountData(Guid Id);
}