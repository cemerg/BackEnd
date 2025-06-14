using Domain.Entities;

namespace Application.Dtos.BackOffice;

public class BackOfficeCustomerDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Point { get; set; }

    public static BackOfficeCustomerDto FromEntity(Customer customer)
    {
        return new BackOfficeCustomerDto
        {
            Id = customer.Id,
            Name = customer.Name,
            Point = customer.Point,
            CreatedAt = customer.CreatedAt
        };
    }
}


public class BackOfficeTransactionDto
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Point { get; set; }
    public Guid? CustomerId { get; set; }

    public static BackOfficeTransactionDto FromEntity(Transaction transaction)
    {
        return new BackOfficeTransactionDto
        {
            Id = transaction.Id,
            CreatedAt = transaction.CreatedAt,
            CustomerId = transaction.CustomerId,
            Point = transaction.Point
        };
    }
}