namespace Domain.Events;

public class OrderCreatedEvent
{
    public Guid OrderId { get; }
    public string CustomerName { get; }

    public OrderCreatedEvent(Guid orderId, string customerName)
    {
        OrderId = orderId;
        CustomerName = customerName;
    }
}
