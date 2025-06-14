using Domain.Entities;

namespace Application.Dtos.WebSite;

public class CustomerDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public int Points { get; set; }

    public static CustomerDto FromEntity(Customer customer)
    {
        return new CustomerDto
        {
            Id = customer.Id,
            Name = customer.Name,
            Points = customer.Point
        };
    }
}

public class TransactionDto
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; } = string.Empty;
    public Guid CustomerId { get; set; }

    internal static TransactionDto FromEntity(Transaction t)
    {
        return new TransactionDto
        {
            Id = t.Id,
            Date = t.CreatedAt,
        };
    }
}