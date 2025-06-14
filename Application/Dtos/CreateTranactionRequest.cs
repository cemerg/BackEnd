public class CreateTransactionRequest
{
    public List<TransactionProductRequest> TransactionProducts { get; set; } = new List<TransactionProductRequest>();
}

public class TransactionProductRequest
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}

public class SetTransactionToCustomerRequest
{
    public Guid CustomerId { get; set; }
    public Guid TransactionId { get; set; }
}


public class RedeemPointsRequest
{
    public int Points { get; set; }
}