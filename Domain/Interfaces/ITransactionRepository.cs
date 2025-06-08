using Domain.Entities;

public interface ITransactionRepository
{
    Task AddAsync(Transaction transaction);
    Task<IEnumerable<Transaction>> GetByCustomerIdAsync(Guid customerId);
    Task<Transaction?> GetByIdAsync(Guid transactionId);

}

public interface ICustomerRepository
{
    Task AddAsync(Customer customer);
    Task<Customer?> GetByIdAsync(Guid customerId);
    Task<Customer?> GetByExternalIdAsync(string externalId);
}


public interface IUnitOfWork
{
    ITransactionRepository TransactionRepository { get; }
    ICustomerRepository CustomerRepository { get; }
    IProductRepository ProductRepository { get; }
    Task SaveChangesAsync();
}
