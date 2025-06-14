using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/product")]
[AdminIdentity]
public class ProductController(IProductService productService) : BaseController
{

    [HttpPost]
    [ProducesResponseType(typeof(ProductDto), 200)]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest createProductRequest)
    {
        var productId = await productService.CreateProduct(createProductRequest);
        return Ok(await productService.GetProductById(productId));
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductDto>), 200)]
    public async Task<IActionResult> GetProducts([FromQuery] Pagination pagination)
    {
        var products = await productService.GetProducts(pagination);
        return Ok(products);
    }


    [HttpGet("{productId}")]
    [ProducesResponseType(typeof(ProductDto), 200)]
    public async Task<IActionResult> GetProductById(Guid productId)
    {
        var product = await productService.GetProductById(productId);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    [HttpDelete("{productId}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> DeleteProduct(Guid productId)
    {
        await productService.DeleteProduct(productId);
        return NoContent();
    }

    [HttpPut("{productId}")]
    [ProducesResponseType(typeof(ProductDto), 200)]
    public async Task<IActionResult> UpdateProduct(Guid productId, [FromBody] UpdateProductRequest updateProductRequest)
    {
        var product = await productService.GetProductById(productId);
        if (product == null)
        {
            return NotFound();
        }

        await productService.UpdateProduct(productId, updateProductRequest);
        return Ok(await productService.GetProductById(productId));
    }

}
