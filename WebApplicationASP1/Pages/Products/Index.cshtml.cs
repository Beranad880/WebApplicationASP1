using Microsoft.AspNetCore.Mvc;
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

    [BindProperty(SupportsGet = true)]
    public string? SearchString { get; set; }

    public async Task OnGetAsync()
    {
        var allProducts = await _productService.GetAllAsync();
        
        if (!string.IsNullOrEmpty(SearchString))
        {
            Products = allProducts
                .Where(s => s.Name.Contains(SearchString, StringComparison.OrdinalIgnoreCase) 
                         || s.Description.Contains(SearchString, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
        else
        {
            Products = allProducts;
        }
    }
}
