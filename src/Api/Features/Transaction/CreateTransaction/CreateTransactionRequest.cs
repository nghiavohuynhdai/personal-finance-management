namespace Api.Features.Transaction.CreateTransaction;

public class CreateTransactionRequest
{
    public string Type { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public string Time { get; set; }
    public Guid CategoryId { get; set; }
    public Guid AccountId { get; set; }
}