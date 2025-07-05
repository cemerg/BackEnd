using Application.Dtos.BackOffice;
using Domain.Entities;

public class AdminService(IAdminContext adminContext, IUnitOfWork unitOfWork) : IAdminService
{
    public async Task<Guid> CreateTransaction(CreateTransactionRequest createTransactionRequest)
    {
        var transaction = Transaction.Create(TransactionType.Purchase, createTransactionRequest.CustomerId);

        var transactionProducts = new List<TransactionProduct>();
        foreach (var productRequest in createTransactionRequest.TransactionProducts)
        {
            var product = await unitOfWork.ProductRepository.GetByIdAsync(productRequest.ProductId);
            if (product == null)
            {
                throw new ArgumentException($"Product with ID {productRequest.ProductId} does not exist.");
            }

            var transactionProduct = TransactionProduct.Create(transaction.Id, product, productRequest.Quantity);
            transactionProducts.Add(transactionProduct);
        }

        transaction.SetTransactionProducts(transactionProducts);

        await unitOfWork.TransactionRepository.AddAsync(transaction);
        await unitOfWork.SaveChangesAsync();
        return transaction.Id;
    }

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

    public async Task<BackOfficeTransactionDto?> GetTransactionById(Guid transactionId)
    {
        var transaction = await unitOfWork.TransactionRepository.GetByIdAsync(transactionId);
        if (transaction == null)
        {
            return null;
        }
        return BackOfficeTransactionDto.FromEntity(transaction);
    }

    public async Task<IEnumerable<BackOfficeTransactionDto>> GetTransactionsByCustomerGuid(Guid customerId, Pagination pagination)
    {
        var customer = await unitOfWork.CustomerRepository.GetByIdAsync(customerId);
        if (customer == null)
        {
            return new List<BackOfficeTransactionDto>();
        }

        var transactions = customer.Transactions
            .Skip(pagination.Skip)
            .Take(pagination.Take)
            .Select(BackOfficeTransactionDto.FromEntity);

        return transactions.ToList();
    }

    public async Task<BackOfficeTransactionDto?> SetTransactionToCustomer(SetTransactionToCustomerRequest setCustomerRequest)
    {
        var transaction = await unitOfWork.TransactionRepository.GetByIdAsync(setCustomerRequest.TransactionId);
        if (transaction == null)
        {
            return null;
        }

        try
        {
            transaction.SetCustomer(setCustomerRequest.CustomerId);

        }
        catch (InvalidOperationException ex)
        {
            throw new BusinessException("Transaction cannot be reassigned.", ex);
        }
        await unitOfWork.SaveChangesAsync();

        return BackOfficeTransactionDto.FromEntity(transaction);
    }
}