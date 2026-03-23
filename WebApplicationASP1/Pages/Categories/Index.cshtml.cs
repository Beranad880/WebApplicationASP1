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
}
