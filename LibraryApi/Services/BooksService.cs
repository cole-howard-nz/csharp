using Microsoft.EntityFrameworkCore;
using LibraryApi.Mappings;
using LibraryApi.Models;
using LibraryApi.Data;
using LibraryApi.Dtos;

namespace LibraryApi.Services;

public class BooksService(LibraryDbContext db) : IBooksService
{
    public async Task<List<Book>> GetAllAsync(Genre? genre, CancellationToken ct)
    {
        IQueryable<Book> query = db.Books.AsNoTracking(); 
        if (genre is not null)
        {
            query = query.Where(book => book.Genre == genre);
        }

        return await query.OrderBy(book => book.Id).ToListAsync(ct);
    }

    public async Task<Book?> GetByIdAsync(int id, CancellationToken ct)
        => await db.Books.AsNoTracking().FirstOrDefaultAsync(book => book.Id == id, ct);

    public async Task<Book> CreateAsync(CreateBookDto createBookDto, CancellationToken ct)
    {
        Book book = createBookDto.ToEntity();
        db.Books.Add(book);
        await db.SaveChangesAsync(ct);

        return book;
    }

    public async Task<bool> UpdateAsync(int id, UpdateBookDto updateBookDto, CancellationToken ct)
    {
        Book? book = await db.Books.FirstOrDefaultAsync(book => book.Id == id, ct);

        if (book is null)
        {
            return false;
        }

        updateBookDto.ApplyTo(book);
        await db.SaveChangesAsync(ct);

        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct)
    {
        Book? book = await db.Books.FirstOrDefaultAsync(book => book.Id == id, ct);

        if (book is null)
        {
            return false;
        }

        db.Books.Remove(book);
        await db.SaveChangesAsync(ct);

        return true;
    }
}