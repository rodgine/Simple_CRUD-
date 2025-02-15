using API.Models;
using Microsoft.EntityFrameworkCore;


namespace API.Data
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Developer> Developers { get; set; }
    }
}
