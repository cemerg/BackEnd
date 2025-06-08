public class CustomerDto
{
    public Guid Id { get; set; }
    public int Point { get; internal set; }

    public static CustomerDto FromEntity(Domain.Entities.Customer customer)
    {
        return new CustomerDto
        {
            Id = customer.Id,
            Point = customer.Point
        };
    }
}

public class CustomerTransactionsDto
{
    public Guid Id { get; set; }
    public string? ExternalId { get; set; }
    public IEnumerable<TransactionDto> Transactions { get; set; } = new List<TransactionDto>();

    public static CustomerTransactionsDto FromEntity(Domain.Entities.Customer customer)
    {
        return new CustomerTransactionsDto
        {
            Id = customer.Id,
            ExternalId = customer.ExternalId,
            Transactions = customer.Transactions.Select(TransactionDto.FromEntity)
        };
    }
}