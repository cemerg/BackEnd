
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


public interface IAdminService
{
    Task<IEnumerable<CustomerDto>> GetAllCustomers();
    Task SetCustomer(SetCustomerRequest request);
    Task<IEnumerable<TransactionDto>> GetAllTransactions();
    Task<CustomerDto?> GetCustomerById(Guid customerId);
    Task<TransactionDto> GetTransactionById(Guid transactionId);
    Task<IEnumerable<TransactionDto>> GetTransactionsByCustomerGuid(Guid customerId);
}