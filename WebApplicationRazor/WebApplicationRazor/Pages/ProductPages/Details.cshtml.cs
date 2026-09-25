using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplicationRazor.Models;
using WebApplicationRazor.Data;

namespace WebApplicationRazor.Pages.ProductPages;

public class DetailsModel : PageModel
{
    private readonly ProductDbContext _context;
    public DetailsModel(ProductDbContext context)
    {
        _context = context;
    }

    public Product Product { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var product = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);
        if (product is null)
        {
            return NotFound();
        }
        else
        {
            Product = product;
        }

        return Page();
    }
}
