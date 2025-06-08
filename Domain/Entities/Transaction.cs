
namespace Domain.Entities;

public class Transaction : BaseEntity, IDateTimeEntity
{
    public int Point { get; set; }
    public TransactionType Type { get; set; }
    public Customer? Customer { get; set; }
    public Guid? CustomerId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<TransactionProduct> TransactionProducts { get; set; } = new List<TransactionProduct>();

    public static Transaction Create(TransactionType transactionType)
    {
        return new Transaction
        {
            Id = Guid.NewGuid(),
            Type = transactionType,
            CreatedAt = DateTime.UtcNow,
        };
    }

    public void Redeem(Guid customerId, int points)
    {
        CustomerId = customerId;
        Point = -1 * points;
    }

    public void SetCustomer(Guid customerId)
    {
        CustomerId = customerId;
    }

    public void SetTransactionProducts(List<TransactionProduct> transactionProducts)
    {
        TransactionProducts = transactionProducts;
        Point = transactionProducts.Sum(tp => tp.Product?.Point * tp.Quantity ?? 0);
    }
}