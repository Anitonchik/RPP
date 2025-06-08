using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RPP.Models;

namespace RPP;

public class RPPDbContext : DbContext
{

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql("");
        base.OnConfiguring(options);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<OrderProduct>().HasKey(x => new
        {
            x.OrderId,
            x.ProductId
        });
    }

    public DbSet<Buyer> Buyers { get; set; }
    public DbSet<JobTitle> JobTitles { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductHistory> ProductHistories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }
    public DbSet<Worker> Workers { get; set; }
}
