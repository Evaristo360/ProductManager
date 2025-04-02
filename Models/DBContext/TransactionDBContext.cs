using Microsoft.EntityFrameworkCore;

namespace ProductManager.Models.DBContext
{
    public class TransactionDBContext : DbContext
    {
        public TransactionDBContext(DbContextOptions<TransactionDBContext> options) : base(options)
        {
        }

        public DbSet<Transaction> Transactions { get; set; }
    }
}
