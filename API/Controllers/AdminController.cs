using Application.Dtos.BackOffice;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/admin")]
[AdminIdentity]
public class AdminController(IAdminService adminService) : BaseController
{

    [HttpGet("customers")]
    [ProducesResponseType(typeof(IEnumerable<BackOfficeCustomerDto>), 200)]
    public async Task<IActionResult> GetCustomers([FromQuery] Pagination pagination)
    {
        return Ok(await adminService.GetAllCustomers(pagination));
    }

    [HttpGet("customers/{customerId}")]
    [ProducesResponseType(typeof(BackOfficeCustomerDto), 200)]
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
    [ProducesResponseType(typeof(IEnumerable<BackOfficeTransactionDto>), 200)]
    public async Task<IActionResult> GetCustomerTransactions(Guid customerId, [FromQuery] Pagination pagination)
    {
        return Ok(await adminService.GetTransactionsByCustomerGuid(customerId, pagination));
    }

    [HttpGet("transactions/{transactionId}")]
    [ProducesResponseType(typeof(BackOfficeTransactionDto), 200)]
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
    [ProducesResponseType(typeof(BackOfficeTransactionDto), 200)]
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
    [ProducesResponseType(typeof(IEnumerable<BackOfficeTransactionDto>), 200)]
    public async Task<IActionResult> GetAllTransactions([FromQuery] Pagination pagination)
    {
        var transactions = await adminService.GetAllTransactions(pagination);
        return Ok(transactions);
    }

    [HttpPost("transaction")]
    [ProducesResponseType(typeof(BackOfficeTransactionDto), 200)]
    public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionRequest createTransactionRequest)
    {
        var transactionId = await adminService.CreateTransaction(createTransactionRequest);
        var transaction = await adminService.GetTransactionById(transactionId);
        return Ok(transaction);
    }

}
