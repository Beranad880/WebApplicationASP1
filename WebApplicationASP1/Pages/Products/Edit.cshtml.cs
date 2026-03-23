using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplicationASP1.Models;
using WebApplicationASP1.Services;

namespace WebApplicationASP1.Pages.Products;

public class EditModel : PageModel
{
    private readonly ProductService _productService;
    private readonly CategoryService _categoryService;

    public EditModel(ProductService productService, CategoryService categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }

    [BindProperty]
    public Product Product { get; set; } = new();

    public SelectList Categories { get; set; }

    public async Task<IActionResult> OnGetAsync(string id)
    {
        var product = await _productService.GetByIdAsync(id);
        if (product == null) return NotFound();
        Product = product;

        var categories = await _categoryService.GetAllAsync();
        Categories = new SelectList(categories, "Name", "Name");

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            var categories = await _categoryService.GetAllAsync();
            Categories = new SelectList(categories, "Name", "Name");
            return Page();
        }

        if (Product.Id != null)
        {
            await _productService.UpdateAsync(Product.Id, Product);
        }

        return RedirectToPage("./Index");
    }
}
