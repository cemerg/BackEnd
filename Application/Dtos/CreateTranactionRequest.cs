public class CreateTransactionRequest
{
    public List<TransactionProductRequest> TransactionProducts { get; set; } = new List<TransactionProductRequest>();
}

public class TransactionProductRequest
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}

public class SetCustomerRequest
{
    public Guid CustomerId { get; set; }
    public Guid TransactionId { get; set; }
}

public class RegisterCustomerRequest
{
    public Guid CustomerId { get; set; }
}

public class RedeemPointsRequest
{
    public Guid CustomerId { get; set; }
    public int Points { get; set; }
}