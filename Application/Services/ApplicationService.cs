using Application.Dtos.BackOffice;
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

public class AdminService(IAdminContext adminContext, IUnitOfWork unitOfWork) : IAdminService
{
    public async Task<IEnumerable<BackOfficeCustomerDto>> GetAllCustomers(Pagination pagination)
    {
        var customers = await unitOfWork.CustomerRepository.GetAllAsync(pagination.Skip, pagination.Take);
        if (customers == null || !customers.Any())
        {
            return new List<BackOfficeCustomerDto>();
        }
        return customers.Select(BackOfficeCustomerDto.FromEntity).ToList();
    }

    public async Task<IEnumerable<BackOfficeTransactionDto>> GetAllTransactions(Pagination pagination)
    {
        var transactions = await unitOfWork.TransactionRepository.GetAllAsync(pagination.Skip, pagination.Take);
        if (transactions == null || !transactions.Any())
        {
            return new List<BackOfficeTransactionDto>();
        }
        return transactions.Select(BackOfficeTransactionDto.FromEntity).ToList();
    }

    public async Task<BackOfficeCustomerDto?> GetCustomerById(Guid customerId)
    {
        var customer = await unitOfWork.CustomerRepository.GetByIdAsync(customerId);
        if (customer == null)
        {
            return null;
        }
        return BackOfficeCustomerDto.FromEntity(customer);
    }

    public async Task<Application.Dtos.BackOffice.BackOfficeTransactionDto?> GetTransactionById(Guid transactionId)
    {
        var transaction = await unitOfWork.TransactionRepository.GetByIdAsync(transactionId);
        if (transaction == null)
        {
            return null;
        }
        return Application.Dtos.BackOffice.BackOfficeTransactionDto.FromEntity(transaction);
    }

    public async Task<IEnumerable<Application.Dtos.BackOffice.BackOfficeTransactionDto>> GetTransactionsByCustomerGuid(Guid customerId, Pagination pagination)
    {
        var customer = await unitOfWork.CustomerRepository.GetByIdAsync(customerId);
        if (customer == null)
        {
            return new List<Application.Dtos.BackOffice.BackOfficeTransactionDto>();
        }

        var transactions = customer.Transactions
            .Skip(pagination.Skip)
            .Take(pagination.Take)
            .Select(Application.Dtos.BackOffice.BackOfficeTransactionDto.FromEntity);

        return transactions.ToList();
    }

    public async Task<Application.Dtos.BackOffice.BackOfficeTransactionDto?> SetTransactionToCustomer(SetTransactionToCustomerRequest setCustomerRequest)
    {
        var transaction = await unitOfWork.TransactionRepository.GetByIdAsync(setCustomerRequest.TransactionId);
        if (transaction == null)
        {
            return null;
        }

        transaction.SetCustomer(setCustomerRequest.CustomerId);
        await unitOfWork.SaveChangesAsync();

        return Application.Dtos.BackOffice.BackOfficeTransactionDto.FromEntity(transaction);
    }
}


public class CustomerService(ICustomerContext customerContext, IUnitOfWork unitOfWork) : ICustomerService
{
    public async Task<Application.Dtos.WebSite.CustomerDto?> GetCustomer()
    {
        var customer = await unitOfWork.CustomerRepository.GetByIdAsync(customerContext.CustomerId);
        if (customer == null)
        {
            return null;
        }
        return Application.Dtos.WebSite.CustomerDto.FromEntity(customer);
    }

    public async Task<IEnumerable<Application.Dtos.WebSite.TransactionDto>> GetTransactions()
    {
        var customer = await unitOfWork.CustomerRepository.GetByIdAsync(customerContext.CustomerId);
        if (customer == null)
        {
            return new List<Application.Dtos.WebSite.TransactionDto>();
        }
        return customer.Transactions.Select(t => Application.Dtos.WebSite.TransactionDto.FromEntity(t)).ToList();
    }

    public async Task RedeemPoints(RedeemPointsRequest request)
    {
        var customer = await unitOfWork.CustomerRepository.GetByIdAsync(customerContext.CustomerId);
        if (customer == null)
        {
            throw new ArgumentException("Customer not found", nameof(customerContext.CustomerId));
        }

        customer.RedeemPoints(request.Points);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task RegisterCustomer(string? externalId = null)
    {
        var existingCustomer = await unitOfWork.CustomerRepository.GetByIdAsync(customerContext.CustomerId);
        if (existingCustomer != null)
        {
            throw new InvalidOperationException("Customer already exists with the provided CustomerId.");
        }

        if (!string.IsNullOrEmpty(externalId))
        {
            existingCustomer = await unitOfWork.CustomerRepository.GetByExternalIdAsync(externalId);
            if (existingCustomer != null)
            {
                throw new InvalidOperationException("Customer already exists with the provided externalId.");
            }
        }

        var customer = Customer.Create(customerContext.CustomerId, externalId);
        await unitOfWork.CustomerRepository.AddAsync(customer);
        await unitOfWork.SaveChangesAsync();
    }
}