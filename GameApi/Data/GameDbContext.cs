using GameApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GameApi.Data;

public class GameDbContext(DbContextOptions<GameDbContext> options) : DbContext(options)
{
    public DbSet<Character> Characters => Set<Character>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Character>(entity =>
        {
           entity.Property(c => c.Name).IsRequired().HasMaxLength(20); 
           entity.Property(c => c.Class).HasConversion<string>().HasMaxLength(20); 
           entity.Property(c => c.AdminNote).HasMaxLength(200); 
        });
    }
}
