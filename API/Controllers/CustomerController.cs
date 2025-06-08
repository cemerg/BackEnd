using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/customer")]
[Produces("application/json")]
public class CustomerController(ICustomerService customerService) : ControllerBase
{

    [HttpGet("transactions")]
    public async Task<IActionResult> GetTransactions([FromQuery] Guid customerId)
    {
        var transactions = await customerService.GetTransactionsByCustomerGuid(customerId);
        return Ok(transactions);
    }


    [HttpGet("overview")]
    public async Task<IActionResult> Get([FromQuery] Guid customerId)
    {
        var transactions = await customerService.GetOverviewByCustomerId(customerId);
        return Ok(transactions);
    }


    [HttpPost("register-with-google")]
    [Authorize]
    public async Task<IActionResult> RegisterWithGoogle([FromBody] RegisterCustomerRequest request)
    {
        var externalId = User.FindFirst("sub")?.Value;
        await customerService.RegisterCustomer(request, externalId);
        return Ok();
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterCustomerRequest request)
    {
        await customerService.RegisterCustomer(request);
        return Ok();
    }

    [HttpPost("redeem-points")]
    public async Task<IActionResult> RedeemPoints([FromBody] RedeemPointsRequest request)
    {
        await customerService.RedeemPoints(request);
        return Ok();
    }


}