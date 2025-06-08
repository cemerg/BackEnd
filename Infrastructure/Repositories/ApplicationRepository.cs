using Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class ApplicationRepository : IApplicationRepository
{
    private readonly ApplicationDbContext _context;

    public ApplicationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Configuration?> GetApplicationConfiguration()
    {
        return await _context.Configurations.FirstOrDefaultAsync();
    }
}

public class ProductRepository  : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CreateProduct(Product product)
    {
        await _context.Set<Product>().AddAsync(product);
    }

    public async Task<Product?> GetByIdAsync(Guid productId)
    {
        return await _context.Set<Product>().FindAsync(productId);
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        return await _context.Set<Product>().ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}