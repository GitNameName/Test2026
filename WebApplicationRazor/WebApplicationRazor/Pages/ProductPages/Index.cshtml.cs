using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplicationRazor.Models;
using WebApplicationRazor.Data;

namespace WebApplicationRazor.Pages.ProductPages;

public class IndexModel : PageModel
{
    private readonly ProductDbContext _context;

    public IndexModel(ProductDbContext context)
    {
        _context = context;
    }

    //Pagination properties
    public int PageSize { get; set; } = 3;
    public int PageNumber { get; set; } = 1;
    public int totalPages { get; set; }

    public bool hasPreviousPage => PageNumber > 1;
    public bool hasNextPage => PageNumber < totalPages;

    //Search properties
    [BindProperty(SupportsGet = true)]
    public string SearchString { get; set; } = string.Empty;

    public IList<Product> Product { get; set; } = default!;

    public async Task OnGetAsync(int pageNumber = 1, string sortOrder = "name_asc")
    {
        //Product = await _context.Products.ToListAsync();
        IQueryable<Product> productsQ = _context.Products;
        ViewData["NameSort"] = sortOrder == "name_asc" ? "name_desc" : "name_asc";
        ViewData["PriceSort"] = sortOrder == "price_asc" ? "price_desc" : "price_asc";

        //Search functionality
        if (!string.IsNullOrEmpty(SearchString))
        {
            productsQ = productsQ.Where(p => p.Name.Contains(SearchString));
        }
        //Sorting functionality
        switch (sortOrder)
        {
            case "name_desc":
                productsQ = productsQ.OrderByDescending(p => p.Name);
                break;
            case "price_asc":
                productsQ = productsQ.OrderBy(p => p.Price);
                break;
            case "price_desc":
                productsQ = productsQ.OrderByDescending(p => p.Price);
                break;
            default:
                productsQ = productsQ.OrderBy(p => p.Name);
                break;
        }
        //Pagination functionality
        PageNumber = pageNumber;
        int totalProducts = await productsQ.CountAsync();
        productsQ = productsQ.Skip((PageNumber - 1) * PageSize).Take(PageSize);
        totalPages = (int)Math.Ceiling(totalProducts / (double)PageSize);
        Product = await productsQ.ToListAsync();



    }
}
