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
}
