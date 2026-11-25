using LibraryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Data;

public class LibraryDbContext(DbContextOptions<LibraryDbContext> options) : DbContext(options)
{
    public DbSet<Book> Books => Set<Book>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity => {
            entity.Property(b => b.Title).HasMaxLength(200);
            entity.Property(b => b.Author).HasMaxLength(100);
            entity.Property(b => b.Genre).HasConversion<string>().HasMaxLength(20);
            entity.Property(b => b.ShelfNote).HasMaxLength(200); 

            entity.HasData(
                new() { Id=1, Title="The Hobbit", Author="J.R.R Tolkien", Genre=Genre.Fantasy, PublishedYear=1937, CopiesAvailable=3, AddedAt=new DateTime(2026, 01, 01, 0, 0, 0, DateTimeKind.Utc), ShelfNote="Fragile spine" },
                new() { Id=2, Title="A Brief History of Time", Author="Stephen Hawking", Genre=Genre.Science, PublishedYear=1988, CopiesAvailable=0, AddedAt=new DateTime(2026, 01, 02, 0, 0, 0, DateTimeKind.Utc) },
                new() { Id=3, Title="Sapiens", Author="Yuval Noah Harari", Genre=Genre.History, PublishedYear=2011, CopiesAvailable=2, AddedAt=new DateTime(2026, 01, 03, 0, 0, 0, DateTimeKind.Utc), ShelfNote="Display copy" }
            );
        });     
    }
} 