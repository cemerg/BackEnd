public class CustomerDto
{
    public Guid Id { get; set; }
    public int Point { get; internal set; }

    public static CustomerOverviewResponse FromEntity(Domain.Entities.Customer customer)
    {
        return new CustomerOverviewResponse
        {
            CustomerId = customer.Id,
            CustomerPoints = customer.Point,
            Email = null,
            Name =null,
            RecentTransactions = customer.Transactions
                .OrderByDescending(t => t.CreatedAt)
                .Take(5)
                .Select(t => TransactionDto.FromEntity(t)).ToList()
        };
    }
}

public class CustomerOverviewResponse
{
    public Guid CustomerId { get; set; }
    public int CustomerPoints { get; set; }
    public string? Email { get; set; }
    public string? Name { get; set; }
    public List<TransactionDto> RecentTransactions { get; set; } = new List<TransactionDto>();
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