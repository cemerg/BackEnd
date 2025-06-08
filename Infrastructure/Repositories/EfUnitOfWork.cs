public class EfUnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private ITransactionRepository? _transactionRepository;
    private ICustomerRepository? _customerRepository;
    private IProductRepository? _productRepository;

    public EfUnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public ITransactionRepository TransactionRepository => _transactionRepository ??= new TransactionRepository(_context);

    public ICustomerRepository CustomerRepository => _customerRepository ??= new CustomerRepository(_context);

    public IProductRepository ProductRepository => _productRepository ??= new ProductRepository(_context);

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}