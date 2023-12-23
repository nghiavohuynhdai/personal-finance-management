using Api.Common;

namespace Api.Entities;

public class Account : Entity
{
    public string Name { get; set; }
    public decimal Balance { get; set; }
    public decimal TotalLoan { get; set; }
    public AccountStatus Status { get; set; } = AccountStatus.Active;
}