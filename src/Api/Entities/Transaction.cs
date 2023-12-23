using Api.Common;

namespace Api.Entities;

public class Transaction : Entity
{
    public TransactionType Type { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public DateTime Time { get; set; }
    public Guid CategoryId { get; set; }
    public Guid AccountId { get; set; }
}