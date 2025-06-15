using Application.Dtos.WebSite;

public interface ICustomerService
{
    Task<CustomerDto?> GetCustomer();
    Task<TransactionDto?> GetTransaction(Guid transactionId);
    Task<IEnumerable<TransactionDto>> GetTransactions();
    Task RedeemPoints(RedeemPointsRequest request);
    Task RegisterCustomer(string? externalId = null);
    Task SetTransaction(SetTransactionRequest setTransactionRequest);
}
