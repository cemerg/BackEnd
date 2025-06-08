using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/transaction")]
[Produces("application/json")]
public class TransactionController(ITransactionService transactionService) : ControllerBase
{

    [HttpPost("create")]
    [ProducesResponseType(typeof(TransactionDto), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionRequest request)
    {
        var response = await transactionService.CreateTransaction(request);
        return Ok(response);
    }


    [HttpPost("set-customer")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> SetCustomer([FromBody] SetCustomerRequest request)
    {
        await transactionService.SetCustomer(request);
        return Ok();
    }
}