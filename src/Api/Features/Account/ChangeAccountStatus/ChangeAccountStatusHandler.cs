using Api.Common;
using Api.Exceptions;
using FluentValidation;

namespace Api.Features.Account.ChangeAccountStatus;

public class ChangeAccountStatusHandler
{
    private readonly IAccountRepository _repository;
    private readonly IValidator<ChangeAccountStatusRequest> _validator;

    public ChangeAccountStatusHandler(IAccountRepository repository, IValidator<ChangeAccountStatusRequest> validator)
    {
        _repository = repository;
        _validator = validator;
    }
    public async Task<ChangedAccountStatusData> Handle(ChangeAccountStatusRequest request, CancellationToken cancellationToken)
    {
        await ValidateRequest(request, cancellationToken);

        await _repository.ChangeAccountStatusAsync(request.Id, Enum.Parse<AccountStatus>(request.Status), cancellationToken);

        return new ChangedAccountStatusData(request.Id);
    }

    private async Task ValidateRequest(ChangeAccountStatusRequest request, CancellationToken cancellationToken)
    {
        var validatedResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validatedResult.IsValid)
        {
            throw new BadRequestException(validatedResult.Errors[0].ErrorMessage);
        }

        if (!await _repository.IsAccountExistAsync(request.Id, cancellationToken))
        {
            throw new NotFoundException("Account not found");
        }
    }

    public record ChangedAccountStatusData(Guid Id);
}
