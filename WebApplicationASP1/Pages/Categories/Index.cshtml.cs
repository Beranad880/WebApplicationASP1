using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplicationASP1.Models;
using WebApplicationASP1.Services;

namespace WebApplicationASP1.Pages.Categories;

public class IndexModel : PageModel
{
    private readonly CategoryService _categoryService;

    public IndexModel(CategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public List<Category> Categories { get; set; } = new();

    public async Task OnGetAsync()
    {
        Categories = await _categoryService.GetAllAsync();
    }

    public async Task<IActionResult> OnPostDeleteAsync(string id)
    {
        await _categoryService.DeleteAsync(id);
        return RedirectToPage();
    }
}
