using Api.Common;
using Api.Data.Repositories.Interfaces;
using Api.Data.UnitOfWork;

namespace Api.Features.Account.GetAccountDetail;

public class GetAccountDetailHandler
{
    private readonly IAccountRepository _repository;

    public GetAccountDetailHandler(IUnitOfWork unitOfWork)
    {
        _repository = unitOfWork.AccountRepository;
    }

    public async Task<AccountDetailData?> Handle(Guid id, CancellationToken cancellationToken)
    {
        return await _repository.GetAccountDetailAsync(id, cancellationToken);
    }
    public record AccountDetailData
    {
        private readonly AccountStatus _status;

        public AccountDetailData(AccountStatus status)
        {
            _status = status;
        }

        public Guid Id { get; init; }
        public string Name { get; init; }
        public decimal Balance { get; init; }
        public decimal TotalLoan { get; set; }
        public string Status => Enum.GetName(_status);
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
