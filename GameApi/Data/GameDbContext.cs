using Microsoft.EntityFrameworkCore;
using GameApi.Models;

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

        modelBuilder.Entity<Character>().HasData(
            new Character { Id = 1, Name = "Brynn", Class = CharacterClass.Warrior, Level = 5, Hitpoints = 200,
                Strength = 18, Intelligence = 4, AdminNote = "playtest account",
                CreatedAt = new DateTime(2026, 11, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Character { Id = 2, Name = "Zeph", Class = CharacterClass.Mage, Level = 7, Hitpoints = 140,
                Strength = 5, Intelligence = 19, AdminNote = "top of the leaderboard",
                CreatedAt = new DateTime(2026, 11, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Character { Id = 3, Name = "Nyx", Class = CharacterClass.Rogue, Level = 3, Hitpoints = 120,
                Strength = 9, Intelligence = 8,
                CreatedAt = new DateTime(2026, 11, 1, 0, 0, 0, DateTimeKind.Utc) }
        );
    }
}
