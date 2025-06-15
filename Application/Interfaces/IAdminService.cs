using Application.Dtos.BackOffice;

public interface IAdminService
{
    Task<Guid> CreateTransaction(CreateTransactionRequest createTransactionRequest);
    Task<IEnumerable<BackOfficeCustomerDto>> GetAllCustomers(Pagination pagination);
    Task<IEnumerable<BackOfficeTransactionDto>> GetAllTransactions(Pagination pagination);
    Task<BackOfficeCustomerDto?> GetCustomerById(Guid customerId);
    Task<BackOfficeTransactionDto?> GetTransactionById(Guid transactionId);
    Task<IEnumerable<BackOfficeTransactionDto>> GetTransactionsByCustomerGuid(Guid customerId, Pagination pagination);
    Task<BackOfficeTransactionDto?> SetTransactionToCustomer(SetTransactionToCustomerRequest setCustomerRequest);
}