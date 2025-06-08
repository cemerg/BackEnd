using Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class TransactionRepository : ITransactionRepository
{
    private readonly ApplicationDbContext _context;

    public TransactionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Transaction transaction)
    {
        await _context.Set<Transaction>().AddAsync(transaction);
    }



    public async Task<IEnumerable<Transaction>> GetByCustomerIdAsync(Guid customerId)
    {
        return await _context.Set<Transaction>()
            .Where(t => t.CustomerId == customerId)
            .Include(t => t.Customer)
            .Include(t => t.TransactionProducts)
            .ToListAsync();

    }

    public async Task<Transaction?> GetByIdAsync(Guid transactionId)
    {
        return await _context.Set<Transaction>()
            .Include(t => t.Customer)
            .Include(t => t.TransactionProducts)
            .FirstOrDefaultAsync(t => t.Id == transactionId);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

}

public class CustomerRepository : ICustomerRepository
{
    private readonly ApplicationDbContext _context;
    public CustomerRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Customer customer)
    {
        await _context.Set<Customer>().AddAsync(customer);
    }

    public async Task<Customer?> GetByExternalIdAsync(string externalId)
    {
        return await _context.Set<Customer>()
            .Include(c => c.Transactions)
            .FirstOrDefaultAsync(c => c.ExternalId == externalId);
    }

    public async Task<Customer?> GetByIdAsync(Guid customerId)
    {
        return await _context.Set<Customer>()
            .Include(c => c.Transactions)
            .FirstOrDefaultAsync(c => c.Id == customerId);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
