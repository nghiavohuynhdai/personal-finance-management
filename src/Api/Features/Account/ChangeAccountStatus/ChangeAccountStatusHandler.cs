using Api.Common;
using Api.Data.Repositories.Interfaces;
using Api.Data.UnitOfWork;
using Api.Exceptions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Api.Features.Account.ChangeAccountStatus;

public class ChangeAccountStatusHandler
{
    private readonly IAccountRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<ChangeAccountStatusRequest> _validator;

    public ChangeAccountStatusHandler(IUnitOfWork unitOfWork, IValidator<ChangeAccountStatusRequest> validator)
    {
        _repository = unitOfWork.AccountRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }
    public async Task<ChangedAccountStatusData> Handle(ChangeAccountStatusRequest request, CancellationToken cancellationToken)
    {
        await ValidateRequest(request, cancellationToken);

        await ChangeAccountStatus(request, cancellationToken);

        return new ChangedAccountStatusData(request.Id);
    }

    private async Task ValidateRequest(ChangeAccountStatusRequest request, CancellationToken cancellationToken)
    {
        var validatedResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validatedResult.IsValid)
        {
            throw new BadRequestException(validatedResult.Errors[0].ErrorMessage);
        }
    }

    private async Task ChangeAccountStatus(ChangeAccountStatusRequest request, CancellationToken cancellationToken)
    {
        var account = await _repository.GetAccountById(request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        _repository.ChangeAccountStatus(account!, Enum.Parse<AccountStatus>(request.Status));

        await _unitOfWork.CommitAsync(cancellationToken);
    }

    public record ChangedAccountStatusData(Guid Id);
}
