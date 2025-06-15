using Domain.Entities;

public class ProductService(IUnitOfWork unitOfWork) : IProductService
{
    public async Task<Guid> CreateProduct(CreateProductRequest createProductRequest)
    {
        var product = Product.Create(createProductRequest.Name, createProductRequest.Point);
        await unitOfWork.ProductRepository.CreateProduct(product);
        await unitOfWork.ProductRepository.SaveChangesAsync();
        return product.Id;
    }

    public async Task DeleteProduct(Guid productId)
    {
        var product = await unitOfWork.ProductRepository.GetByIdAsync(productId);
        if (product == null)
        {
            return; // Or throw an exception if you prefer
        }

        unitOfWork.ProductRepository.DeleteProduct(product);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task<ProductDto?> GetProductById(Guid productId)
    {
        var product = await unitOfWork.ProductRepository.GetByIdAsync(productId);
        if (product == null)
        {
            return null;
        }
        return ProductDto.FromEntity(product);
    }

    public async Task<IEnumerable<ProductDto>> GetProducts()
    {
        var products = await unitOfWork.ProductRepository.GetProductsAsync();
        return products.Select(ProductDto.FromEntity);
    }

    public async Task<IEnumerable<ProductDto>> GetProducts(Pagination pagination)
    {
        var products = await unitOfWork.ProductRepository.GetProductsAsync();
        if (products == null || !products.Any())
        {
            return new List<ProductDto>();
        }
        return products.Skip(pagination.Skip).Take(pagination.Take).Select(ProductDto.FromEntity).ToList();
    }

    public async Task UpdateProduct(Guid productId, UpdateProductRequest updateProductRequest)
    {
        var product = await unitOfWork.ProductRepository.GetByIdAsync(productId);
        if (product == null)
        {
            return;
        }

        product.Name = updateProductRequest.Name;
        product.Point = updateProductRequest.Point;

        await unitOfWork.ProductRepository.SaveChangesAsync();
    }
}