
namespace Domain.Entities;

public class Transaction : BaseEntity, IDateTimeEntity
{
    public int Point { get; set; }
    public TransactionType Type { get; set; }
    public Customer? Customer { get; set; }
    public Guid? CustomerId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<TransactionProduct> TransactionProducts { get; set; } = new List<TransactionProduct>();

    public static Transaction Create(TransactionType transactionType, Guid? customerId)
    {
        return new Transaction
        {
            Id = Guid.NewGuid(),
            Type = transactionType,
            CustomerId = customerId
        };
    }

    public void Redeem(Guid customerId, int points)
    {
        CustomerId = customerId;
        Point = -1 * points;
    }

    public void SetCustomer(Guid customerId)
    {
        if (CustomerId.HasValue && CustomerId != customerId)
        {
            throw new InvalidOperationException("Transaction is already assigned to another customer.");
        }
        
        CustomerId = customerId;
    }

    public void SetTransactionProducts(List<TransactionProduct> transactionProducts)
    {
        TransactionProducts = transactionProducts;
        Point = transactionProducts.Sum(tp => tp.Product?.Point * tp.Quantity ?? 0);
    }
}