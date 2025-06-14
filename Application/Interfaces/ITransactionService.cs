using System.Transactions;

namespace Application.Interfaces;

public interface ITransactionService
{
    Task<TransactionDto> CreateTransaction(CreateTransactionRequest createTransactionRequest);
}



public interface ICustomerService
{
    Task<CustomerOverviewResponse> GetOverviewByCustomerId(Guid customerId);
    Task<IEnumerable<TransactionDto>> GetTransactionsByCustomerGuid(Guid customerId);
    Task RedeemPoints(RedeemPointsRequest request);
    Task RegisterCustomer(RegisterCustomerRequest request, string? externalId = null);
}

