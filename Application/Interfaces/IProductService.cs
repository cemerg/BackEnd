public interface IProductService
{
    Task<Guid> CreateProduct(CreateProductRequest createProductRequest);
    Task DeleteProduct(Guid productId);
    Task<ProductDto?> GetProductById(Guid productId);
    Task<IEnumerable<ProductDto>> GetProducts(Pagination pagination);
    Task UpdateProduct(Guid productId, UpdateProductRequest updateProductRequest);
}