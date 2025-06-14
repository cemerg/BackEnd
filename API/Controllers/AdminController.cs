using Application.Dtos.BackOffice;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/admin")]
[AdminIdentity]
public class AdminController(IAdminService adminService) : BaseController
{

    [HttpGet("customers")]
    [ProducesResponseType(typeof(IEnumerable<CustomerDto>), 200)]
    public async Task<IActionResult> GetCustomers([FromQuery] Pagination pagination)
    {
        return Ok(await adminService.GetAllCustomers(pagination));
    }

    [HttpGet("customers/{customerId}")]
    [ProducesResponseType(typeof(CustomerDto), 200)]
    public async Task<IActionResult> GetCustomerById(Guid customerId)
    {
        var customer = await adminService.GetCustomerById(customerId);
        if (customer == null)
        {
            return NotFound();
        }
        return Ok(customer);
    }

    [HttpGet("customers/{customerId}/transactions")]
    [ProducesResponseType(typeof(IEnumerable<TransactionDto>), 200)]
    public async Task<IActionResult> GetCustomerTransactions(Guid customerId, [FromQuery] Pagination pagination)
    {
        return Ok(await adminService.GetTransactionsByCustomerGuid(customerId, pagination));
    }

    [HttpGet("transactions/{transactionId}")]
    [ProducesResponseType(typeof(TransactionDto), 200)]
    public async Task<IActionResult> GetTransactionById(Guid transactionId)
    {
        var transaction = await adminService.GetTransactionById(transactionId);
        if (transaction == null)
        {
            return NotFound();
        }
        return Ok(transaction);
    }

    [HttpPost("set-transaction-to-customer")]
    [ProducesResponseType(typeof(TransactionDto), 200)]
    public async Task<IActionResult> SetTransactionToCustomer([FromBody] SetTransactionToCustomerRequest setCustomerRequest)
    {
        var transaction = await adminService.SetTransactionToCustomer(setCustomerRequest);
        if (transaction == null)
        {
            return NotFound();
        }

        return Ok(transaction);
    }

    [HttpGet("transactions")]
    [ProducesResponseType(typeof(IEnumerable<TransactionDto>), 200)]
    public async Task<IActionResult> GetAllTransactions([FromQuery] Pagination pagination)
    {
        var transactions = await adminService.GetAllTransactions(pagination);
        return Ok(transactions);
    }

}
