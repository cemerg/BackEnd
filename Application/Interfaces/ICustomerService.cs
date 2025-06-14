using Application.Dtos.WebSite;

public interface ICustomerService
{
    Task<CustomerDto?> GetCustomer();
    Task<IEnumerable<TransactionDto>> GetTransactions();
    Task RedeemPoints(RedeemPointsRequest request);
    Task RegisterCustomer(string? externalId = null);
}
