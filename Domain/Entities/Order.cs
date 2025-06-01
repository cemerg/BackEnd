namespace Domain.Entities;

public class Order
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string CustomerName { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public Order(string customerName)
    {
        CustomerName = customerName;
    }
}
