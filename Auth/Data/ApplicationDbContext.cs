using TechnoBit.Models;

namespace TechnoBit.Data;

using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Diğer konfigürasyonlar...

        modelBuilder.Entity<User>()
            .Property(u => u.RefreshTokenExpiryTime)
            .HasColumnType("timestamp with time zone");
    }
}
