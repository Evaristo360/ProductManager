using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace ProductManager.Models.DBContext
{
    public class ProductDBContext : DbContext
    {
        public ProductDBContext(DbContextOptions<ProductDBContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
