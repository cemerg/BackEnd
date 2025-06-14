
namespace Application.Interfaces;

public interface IApplicationService
{
    Task<ApplicationConfigurationDto?> GetApplicationConfiguration();
}

public interface IProductService
{
    Task<Guid> CreateProduct(CreateProductRequest createProductRequest);
    Task DeleteProduct(Guid productId);
    Task<ProductDto?> GetProductById(Guid productId);
    Task<IEnumerable<ProductDto>> GetProducts(Pagination pagination);
    Task UpdateProduct(Guid productId, UpdateProductRequest updateProductRequest);
}


public interface IAdminService
{
    Task<IEnumerable<Dtos.BackOffice.CustomerDto>> GetAllCustomers(Pagination pagination);
    Task<IEnumerable<Dtos.BackOffice.TransactionDto>> GetAllTransactions(Pagination pagination);
    Task<Dtos.BackOffice.CustomerDto?> GetCustomerById(Guid customerId);
    Task<Dtos.BackOffice.TransactionDto?> GetTransactionById(Guid transactionId);
    Task<IEnumerable<Dtos.BackOffice.TransactionDto>> GetTransactionsByCustomerGuid(Guid customerId, Pagination pagination);
    Task<Dtos.BackOffice.TransactionDto?> SetTransactionToCustomer(SetTransactionToCustomerRequest setCustomerRequest);
}



public interface ICustomerService
{
    Task<Dtos.WebSite.CustomerDto?> GetCustomer();
    Task<IEnumerable<Dtos.WebSite.TransactionDto>> GetTransactions();
    Task RedeemPoints(RedeemPointsRequest request);
    Task RegisterCustomer(string? externalId = null);
}

