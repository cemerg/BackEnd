using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public IActionResult GetOrders()
    {
        // This method could be implemented to return a list of orders
        return Ok(new { message = "List of orders" });
    }

    [HttpPost]
    public IActionResult CreateOrder([FromBody] string customerName)
    {
        _orderService.CreateOrder(customerName);
        return Ok(new { message = "Order created" });
    }
}
