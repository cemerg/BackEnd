using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/transaction")]
public class TransactionController(ITransactionService transactionService) : BaseController
{

    [HttpPost("create")]
    [ProducesResponseType(typeof(TransactionDto), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionRequest request)
    {
        var response = await transactionService.CreateTransaction(request);
        return Ok(response);
    }
    
}