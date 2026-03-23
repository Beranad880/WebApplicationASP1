using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplicationASP1.Models;
using WebApplicationASP1.Services;

namespace WebApplicationASP1.Pages.Products;

public class CreateModel : PageModel
{
    private readonly ProductService _productService;

    public CreateModel(ProductService productService)
    {
        _productService = productService;
    }

    [BindProperty]
    public Product Product { get; set; } = new();

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        await _productService.CreateAsync(Product);
        return RedirectToPage("./Index");
    }
}
