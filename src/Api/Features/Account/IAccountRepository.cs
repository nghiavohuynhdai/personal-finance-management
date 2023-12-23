namespace Api.Features.Account;

public interface IAccountRepository
{
    Task<Guid> CreateAccountAsync(Entities.Account account, CancellationToken cancellationToken = default);
    Task<bool> IsNameUniqueAsync(string name, CancellationToken cancellationToken = default);
}