using Microsoft.EntityFrameworkCore;
using WebApplicationRazor.Models;

namespace WebApplicationRazor.Data
{
    public class ProductDbContext : DbContext
    { 
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Products");
        }
    }
}
