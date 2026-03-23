using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplicationASP1.Models;
using WebApplicationASP1.Services;

namespace WebApplicationASP1.Pages.Products;

public class DeleteModel : PageModel
{
    private readonly ProductService _productService;

    public DeleteModel(ProductService productService)
    {
        _productService = productService;
    }

    [BindProperty]
    public Product Product { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(string id)
    {
        var product = await _productService.GetByIdAsync(id);
        if (product == null) return NotFound();
        Product = product;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (string.IsNullOrEmpty(Product.Id)) return NotFound();
        await _productService.DeleteAsync(Product.Id);
        return RedirectToPage("./Index");
    }
}
