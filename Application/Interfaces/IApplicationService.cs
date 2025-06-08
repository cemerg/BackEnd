namespace Application.Interfaces;

public interface IApplicationService
{
    Task<ApplicationConfigurationDto?> GetApplicationConfiguration();
}

public interface IProductService
{
    Task CreateProduct(CreateProductRequest createProductRequest);
    Task<IEnumerable<ProductDto>> GetProducts();
}
