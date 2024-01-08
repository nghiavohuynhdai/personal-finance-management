using Api.Features.Transaction.CreateTransaction;

namespace Api.Features.Transaction;

public static class TransactionEndpoints
{
    
    public static void Map(RouteGroupBuilder groupBuilder)
    {
        var transactionGroupBuilder = groupBuilder.MapGroup("transactions").WithTags("Transaction");
        CreateTransactionEndpoint.Map(transactionGroupBuilder);
    }
}