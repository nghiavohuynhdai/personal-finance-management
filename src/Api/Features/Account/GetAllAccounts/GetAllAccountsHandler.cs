using Api.Common;

namespace Api.Features.Account.GetAllAccounts;

public class GetAllAccountsHandler
{
    private readonly IAccountRepository _repository;

    public GetAllAccountsHandler(IAccountRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<AccountData>> Handle(CancellationToken cancellationToken)
    {
        var accounts = await _repository.GetAllAccountsAsync(cancellationToken);
        return accounts;
    }
    
    public record AccountData
    {
        private readonly AccountStatus _status;

        public AccountData(AccountStatus status)
        {
            _status = status;
        }

        public Guid Id { get; init; }
        public string Name { get; init; }
        public decimal Balance { get; init; }
        public string Status => Enum.GetName(_status);
    }
}