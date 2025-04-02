using Microsoft.EntityFrameworkCore;
using ProductManager.Models;

namespace ProductManager.Models.DBContext
{
    public class LocationDBContext : DbContext
    {
        public LocationDBContext(DbContextOptions<LocationDBContext> options) : base(options)
        {
        }

        public DbSet<Location> Locations { get; set; }
    }
}
