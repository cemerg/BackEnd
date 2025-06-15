using Application.Dtos.WebSite;
using Domain.Entities;
using Domain.Events;

public class CustomerService(ICustomerContext customerContext, IUnitOfWork unitOfWork) : ICustomerService
{
    public async Task<CustomerDto?> GetCustomer()
    {
        var customer = await unitOfWork.CustomerRepository.GetByIdAsync(customerContext.CustomerId);
        if (customer == null)
        {
            return null;
        }
        return CustomerDto.FromEntity(customer);
    }

    public async Task<TransactionDto?> GetTransaction(Guid transactionId)
    {
        var transaction = await unitOfWork.TransactionRepository.GetByIdAsync(transactionId);
        if (transaction == null || transaction.CustomerId != customerContext.CustomerId)
        {
            return null;
        }

        return TransactionDto.FromEntity(transaction);
    }

    public async Task<IEnumerable<TransactionDto>> GetTransactions()
    {
        var customer = await unitOfWork.CustomerRepository.GetByIdAsync(customerContext.CustomerId);
        if (customer == null)
        {
            return new List<TransactionDto>();
        }
        return customer.Transactions.Select(t => TransactionDto.FromEntity(t)).ToList();
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

    public async Task SetTransaction(SetTransactionRequest setTransactionRequest)
    {
        var customer = await unitOfWork.CustomerRepository.GetByIdAsync(customerContext.CustomerId);
        if (customer == null)
        {
            throw new ArgumentException("Customer not found", nameof(customerContext.CustomerId));
        }

        var transaction = await unitOfWork.TransactionRepository.GetByIdAsync(setTransactionRequest.TransactionId);
        if (transaction == null)
        {
            throw new ArgumentException("Transaction not found", nameof(setTransactionRequest.TransactionId));
        }

        if (transaction.CustomerId.HasValue && transaction.CustomerId != customerContext.CustomerId)
        {
            throw new InvalidOperationException("Transaction is already assigned to another customer.");
        }

        transaction.SetCustomer(customerContext.CustomerId);
        await unitOfWork.SaveChangesAsync();

    }
}