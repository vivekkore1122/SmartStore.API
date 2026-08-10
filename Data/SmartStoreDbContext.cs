using Microsoft.EntityFrameworkCore;
using SmartStore.API.Models.Domain;

namespace SmartStore.API.Data;

public class SmartStoreDbContext : DbContext
{
    public SmartStoreDbContext(DbContextOptions<SmartStoreDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Supplier> Suppliers { get; set; }
}