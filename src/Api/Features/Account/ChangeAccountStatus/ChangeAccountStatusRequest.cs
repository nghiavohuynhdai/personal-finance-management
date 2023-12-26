namespace Api.Features.Account.ChangeAccountStatus;

public class ChangeAccountStatusRequest
{
    public Guid Id { get; set; }
    public string Status { get; set; }
}
