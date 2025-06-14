public class CustomerTransactionDto
{
    public Guid Id { get; internal set; }
    public int Point { get; internal set; }
    public TransactionType Type { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string CustomerName { get; private set; }

    public static CustomerTransactionDto FromEntity(Domain.Entities.Transaction transaction, string customerName)
    {
        return new CustomerTransactionDto
        {
            Id = transaction.Id,
            Point = transaction.Point,
            Type = transaction.Type,
            CreatedAt = transaction.CreatedAt,
            CustomerName = customerName
        };
    }

    public Domain.Entities.Transaction ToEntity()
    {
        return new Domain.Entities.Transaction
        {
            Id = Id,
            Point = Point
        };
    }
}