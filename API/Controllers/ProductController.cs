using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/product")]
public class ProductController(IProductService productService) : BaseController
{

    [HttpPost("create")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest createProductRequest)
    {
        await productService.CreateProduct(createProductRequest);
        return Ok();
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductDto>), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetProducts()
    {
        var products = await productService.GetProducts();
        return Ok(products);
    }


   
}
