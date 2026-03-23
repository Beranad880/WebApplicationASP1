using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using WebApplicationASP1.Models;
using WebApplicationASP1.Services;
using WebApplicationASP1.test;

namespace WebApplicationASP1.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class ProductsController : ControllerBase
{
    private readonly ProductService _productService;
    private readonly IMyTax _tax;

    public ProductsController(ProductService productService,IMyTax tax )
    {
        _productService = productService;
        _tax = tax;

    }
    // GET /api/products
    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetAll() =>
        Ok(await _productService.GetAllAsync());

    // GET /api/products/{id}
    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Product>> GetById(string id)
    {
        var product = await _productService.GetByIdAsync(id);
        return product is null ? NotFound() : Ok(product);
    }

    // POST /api/products
    [HttpPost]
    public async Task<ActionResult<Product>> Create(Product product)
    {
        product.CreatedAt = DateTime.UtcNow;
        var created = await _productService.CreateAsync(product);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT /api/products/{id}
    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Product updated)
    {
        var exists = await _productService.GetByIdAsync(id);
        if (exists is null) return NotFound();

        await _productService.UpdateAsync(id, updated);
        return NoContent();
    }

    // DELETE /api/products/{id}
    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var deleted = await _productService.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }

    [HttpGet("{id}/tax")]
    public async Task<IActionResult> GetPriceWithTax(string id)
    {
        var product = await _productService.GetByIdAsync(id);

        if (product is null)
            return NotFound();

        return Ok(new
        {
            productId = id,
            price = product.Price,
            taxRate = _tax.DefaultTaxRate,
            priceWithTax = _tax.GetPriceWithTax(product.Price)
        });
    }
}


