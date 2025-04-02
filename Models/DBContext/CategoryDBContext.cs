using Microsoft.EntityFrameworkCore;

namespace ProductManager.Models.DBContext
{
    public class CategoryDBContext : DbContext
    {
        public CategoryDBContext(DbContextOptions<CategoryDBContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
    }
}
