// Domain/Events/TransactionCreatedEvent.cs
namespace Domain.Events;

public class TransactionCreatedEvent
{
    public Guid TransactionId { get; }
    public int Point { get; }
    public DateTime CreatedAt { get; }

    public TransactionCreatedEvent(Guid transactionId, int point, DateTime createdAt)
    {
        TransactionId = transactionId;
        Point = point;
        CreatedAt = createdAt;
    }
}
