using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplicationASP1.Models;
using WebApplicationASP1.Services;

namespace WebApplicationASP1.Pages.Categories;

public class CreateModel : PageModel
{
    private readonly CategoryService _categoryService;

    public CreateModel(CategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [BindProperty]
    public Category Category { get; set; } = new();

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        await _categoryService.CreateAsync(Category);

        return RedirectToPage("./Index");
    }
}
