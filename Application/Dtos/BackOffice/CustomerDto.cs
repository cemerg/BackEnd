using Domain.Entities;

namespace Application.Dtos.BackOffice;

public class CustomerDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Point { get; set; }

    public static CustomerDto FromEntity(Customer customer)
    {
        return new CustomerDto
        {
            Id = customer.Id,
            Name = customer.Name,
            Point = customer.Point,
            CreatedAt = customer.CreatedAt
        };
    }
}


public class TransactionDto
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public decimal TotalAmount { get; set; }
    public Guid CustomerId { get; set; }

    public static TransactionDto FromEntity(Transaction transaction)
    {
        return new TransactionDto
        {
            Id = transaction.Id,
            CreatedAt = transaction.CreatedAt,
        };
    }
}