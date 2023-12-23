namespace Api.Features.Account.CreateAccount;

public class CreateAccountRequest
{
    public string Name { get; set; }
    public decimal Balance { get; set; }
    public decimal TotalLoan { get; set; }
}