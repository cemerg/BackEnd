using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Dtos.WebSite;

namespace API.Controllers;

[Route("api/customer")]
[CustomerIdentity]
public class CustomerController(ICustomerService customerService) : BaseController
{

    [HttpGet("transactions")]
    [ProducesResponseType(typeof(IEnumerable<TransactionDto>), 200)]
    public async Task<IActionResult> GetTransactions()
    {
        var transactions = await customerService.GetTransactions();
        return Ok(transactions);
    }

    [HttpGet("transactions/{transactionId}")]
    [ProducesResponseType(typeof(TransactionDto), 200)]
    public async Task<IActionResult> GetTransaction(Guid transactionId)
    {
        var transaction = await customerService.GetTransaction(transactionId);
        return Ok(transaction);
    }




    [HttpGet]
    [ProducesResponseType(typeof(CustomerDto), 200)]
    public async Task<IActionResult> GetCustomer()
    {
        var customer = await customerService.GetCustomer();
        if (customer == null)
        {
            return NotFound();
        }

        return Ok(customer);
    }


    [Authorize]
    [HttpPost("register-with-google")]
    [ProducesResponseType(typeof(CustomerDto), 200)]
    public async Task<IActionResult> RegisterWithGoogle()
    {
        var externalId = User.FindFirst("sub")?.Value;
        await customerService.RegisterCustomer(externalId);
        return Ok(await customerService.GetCustomer());

    }


    [HttpPost("register")]
    [ProducesResponseType(typeof(CustomerDto), 200)]
    public async Task<IActionResult> Register()
    {
        await customerService.RegisterCustomer();
        return Ok(await customerService.GetCustomer());
    }

    [HttpPost("redeem-points")]
    [ProducesResponseType(typeof(CustomerDto), 200)]
    public async Task<IActionResult> RedeemPoints([FromBody] RedeemPointsRequest request)
    {
        await customerService.RedeemPoints(request);
        return Ok(await customerService.GetCustomer());
    }

    [HttpPost("set-transaction")]
    [ProducesResponseType(typeof(TransactionDto), 200)]
    public async Task<IActionResult> SetTransaction([FromBody] SetTransactionRequest setTransactionRequest)
    {
        await customerService.SetTransaction(setTransactionRequest);
        var transaction = await customerService.GetTransaction(setTransactionRequest.TransactionId);
        return Ok(transaction);
    }
}