using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/admin")]
public class AdminController(IAdminService adminService) : BaseController
{

    [HttpGet("customers")]
    public async Task<IActionResult> GetCustomers()
    {
        var customers = await adminService.GetAllCustomers();
        return Ok(customers);
    }

    [HttpGet("customers/{customerId}")]
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
    public async Task<IActionResult> GetCustomerTransactions(Guid customerId)
    {
        var transactions = await adminService.GetTransactionsByCustomerGuid(customerId);
        return Ok(transactions);
    }

    [HttpGet("transactions")]
    public async Task<IActionResult> GetTransactions()
    {
        var transactions = await adminService.GetAllTransactions();
        return Ok(transactions);
    }

    [HttpGet("transactions/{transactionId}")]
    public async Task<IActionResult> GetTransactionById(Guid transactionId)
    {
        var transaction = await adminService.GetTransactionById(transactionId);
        if (transaction == null)
        {
            return NotFound();
        }
        return Ok(transaction);
    }

    [HttpPost("transactions/{transactionId}/set-customer")]
    public async Task<IActionResult> SetTransactionCustomer([FromBody] SetCustomerRequest setCustomerRequest)
    {
        await adminService.SetCustomer(setCustomerRequest);
        return Ok();
    }

}
