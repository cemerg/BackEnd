using Application.Interfaces;
using Domain.Entities;

public class ApplicationService : IApplicationService
{
    private readonly IApplicationRepository _applicationRepository;

    public ApplicationService(IApplicationRepository applicationRepository)
    {
        _applicationRepository = applicationRepository;
    }

    public async Task<ApplicationConfigurationDto?> GetApplicationConfiguration()
    {
        var applicationConfiguration = await _applicationRepository.GetApplicationConfiguration();
        return new ApplicationConfigurationDto()
        {
            ApplicationTitle = applicationConfiguration?.ApplicationTitle ?? "Default Application Title",
        };
    }
}

public class ProductService(IProductRepository productRepository) : IProductService
{
    public async Task CreateProduct(CreateProductRequest createProductRequest)
    {
        var product = Product.Create(createProductRequest.Name, createProductRequest.Point);
        await productRepository.CreateProduct(product);
        await productRepository.SaveChangesAsync();
    }

    public async Task<IEnumerable<ProductDto>> GetProducts()
    {
        var products = await productRepository.GetProductsAsync();
        return products.Select(ProductDto.FromEntity);
    }
}