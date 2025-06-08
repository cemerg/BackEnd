namespace Domain.Entities;

public class Customer : BaseEntity
{
    public string? ExternalId { get; set; }
    public int Point { get; set; }
    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public static Customer Create(Guid? guid = null, string? externalId = null)
    {
        return new Customer
        {
            Id = guid ?? Guid.NewGuid(),
            ExternalId = externalId,
            Point = 0
        };
    }

    public void AddTransaction(Transaction transaction)
    {
        if (transaction == null)
        {
            throw new ArgumentNullException(nameof(transaction), "Transaction cannot be null.");
        }

        Transactions.Add(transaction);
        Point += transaction.Point; 
    }

    public void RedeemPoints(int points)
    {
        if (points < 0)
        {
            throw new ArgumentException("Points to redeem must be a positive value.", nameof(points));
        }

        if (Point < points)
        {
            throw new InvalidOperationException("Insufficient points to redeem.");
        }

        Point -= points;
    }
}

