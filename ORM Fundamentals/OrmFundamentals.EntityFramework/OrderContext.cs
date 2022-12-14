using Microsoft.EntityFrameworkCore;
using OrmFundamentals.Shared.Models;

namespace OrmFundamentals.EntityFramework
{
    public sealed class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> contextOptions)
            : base(contextOptions)
        {
        }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().Property(o => o.Status).HasConversion<string>();
        }
    }
}
