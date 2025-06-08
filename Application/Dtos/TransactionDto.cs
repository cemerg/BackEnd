
public class TransactionDto
{
    public Guid Id { get; internal set; }
    public int Point { get; internal set; }
    public TransactionType Type { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public static TransactionDto FromEntity(Domain.Entities.Transaction transaction)
    {
        return new TransactionDto
        {
            Id = transaction.Id,
            Point = transaction.Point,
            Type = transaction.Type,
            CreatedAt = transaction.CreatedAt
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