using Application.Interfaces;
using Domain.Entities;
using Domain.Events;

public class TransactionService(IUnitOfWork unitOfWork, IEventPublisher eventPublisher) : ITransactionService
{
    public async Task<TransactionDto> CreateTransaction(CreateTransactionRequest createTransactionRequest)
    {
        var transaction = Transaction.Create(TransactionType.Purchase);
        var transactionProducts = new List<TransactionProduct>();
        foreach (var transactionProductRequest in createTransactionRequest.TransactionProducts)
        {
            var product = await unitOfWork.ProductRepository.GetByIdAsync(transactionProductRequest.ProductId);
            if (product == null)
            {
                throw new ArgumentException($"Product with ID {transactionProductRequest.ProductId} not found", nameof(transactionProductRequest.ProductId));
            }

            var transactionProduct = TransactionProduct.Create(transaction.Id, product, transactionProductRequest.Quantity);
            transactionProducts.Add(transactionProduct);
        }

        transaction.SetTransactionProducts(transactionProducts);


        await unitOfWork.TransactionRepository.AddAsync(transaction);
        await unitOfWork.SaveChangesAsync();

        var transactionCreatedEvent = new TransactionCreatedEvent(
            transaction.Id,
            transaction.Point,
            transaction.CreatedAt
        );

        eventPublisher.Publish(transactionCreatedEvent);
        return TransactionDto.FromEntity(transaction);
    }

    public async Task SetCustomer(SetCustomerRequest request)
    {
        var transaction = await unitOfWork.TransactionRepository.GetByIdAsync(request.TransactionId);
        if (transaction == null)
        {
            throw new ArgumentException("Transaction not found", nameof(request.TransactionId));
        }

        var customer = await unitOfWork.CustomerRepository.GetByIdAsync(request.CustomerId);
        if (customer == null)
        {
            throw new ArgumentException("Customer not found", nameof(request.CustomerId));
        }

        customer.AddTransaction(transaction);
        await unitOfWork.SaveChangesAsync();
    }
}

public class CustomerService(IUnitOfWork unitOfWork) : ICustomerService
{
    public async Task<CustomerOverviewResponse> GetOverviewByCustomerId(Guid customerId)
    {
        var customer = await unitOfWork.CustomerRepository.GetByIdAsync(customerId);
        if (customer == null)
        {
            return new CustomerOverviewResponse
            {
                CustomerId = customerId,
                CustomerPoints = 0,
                Email = null,
                Name = null,
                RecentTransactions = new List<TransactionDto>()
            };
        }

        return CustomerDto.FromEntity(customer);
    }




    public async Task<IEnumerable<TransactionDto>> GetTransactionsByCustomerGuid(Guid customerId)
    {
        var transactions = await unitOfWork.TransactionRepository.GetByCustomerIdAsync(customerId);
        return transactions.Select(t => TransactionDto.FromEntity(t)).ToList();
    }

    public async Task RedeemPoints(RedeemPointsRequest request)
    {
        var transaction = Transaction.Create(TransactionType.Redeem);
        transaction.Redeem(request.CustomerId, request.Points);

        var customer = await unitOfWork.CustomerRepository.GetByIdAsync(request.CustomerId);
        if (customer == null)
        {
            throw new ArgumentException("Customer not found", nameof(request.CustomerId));
        }
        customer.RedeemPoints(request.Points);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task RegisterCustomer(RegisterCustomerRequest request, string? externalId = null)
    {
        var existingCustomer = await unitOfWork.CustomerRepository.GetByIdAsync(request.CustomerId);
        if (existingCustomer != null)
        {
            throw new ArgumentException("Customer already exists", nameof(request.CustomerId));
        }

        if (!string.IsNullOrEmpty(externalId))
        {
            var existingCustomerByExternalId = await unitOfWork.CustomerRepository.GetByExternalIdAsync(externalId);
            if (existingCustomerByExternalId != null)
            {
                throw new ArgumentException("Customer with this external ID already exists", nameof(externalId));
            }
        }

        var customer = Customer.Create(request.CustomerId, externalId);
        await unitOfWork.CustomerRepository.AddAsync(customer);
        await unitOfWork.SaveChangesAsync();

    }
}

