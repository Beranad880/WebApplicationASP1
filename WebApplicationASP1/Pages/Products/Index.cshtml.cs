using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplicationASP1.Models;
using WebApplicationASP1.Services;

namespace WebApplicationASP1.Pages.Products;

public class IndexModel : PageModel
{
    private readonly ProductService _productService;

    public IndexModel(ProductService productService)
    {
        _productService = productService;
    }

    public List<Product> Products { get; set; } = new();

    public async Task OnGetAsync()
    {
        Products = await _productService.GetAllAsync();
    }
}
