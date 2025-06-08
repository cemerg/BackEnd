
using Domain.Entities;

public interface IApplicationRepository
{
    Task<Configuration?> GetApplicationConfiguration();
}

public interface IProductRepository
{
    Task CreateProduct(Product product);
    Task<Product?> GetByIdAsync(Guid productId);
    Task<IEnumerable<Product>> GetProductsAsync();
    Task SaveChangesAsync();
}