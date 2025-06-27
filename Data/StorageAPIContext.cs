using Microsoft.EntityFrameworkCore;
using StorageAPI.Models;

namespace StorageAPI.Data;
public class StorageAPIContext : DbContext
{
    public StorageAPIContext (DbContextOptions<StorageAPIContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Product { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Bregott", Price = 60, Category = "Mejeri", Shelf = "A1", Count = 40, Description = "500ml"},
            new Product { Id = 2, Name = "Apelsin", Price = 24, Category = "Frukt", Shelf = "B2", Count = 140, Description = "Från Spanien"},
            new Product { Id = 3, Name = "Vetemjöl", Price = 10, Category = "Skafferi", Shelf = "C4", Count = 20, Description = "Ekologiskt vetemjöl"},
            new Product { Id = 4, Name = "Broccoli", Price = 30, Category = "Grönt", Shelf = "B1", Count = 100, Description = "Svenskt"},
            new Product { Id = 5, Name = "Filmjölk", Price = 30, Category = "Mejeri", Shelf = "A2", Count = 20, Description = "1 liter"}
            
            );
    }
}