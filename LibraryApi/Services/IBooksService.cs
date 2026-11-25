using LibraryApi.Dtos;
using LibraryApi.Models;

namespace LibraryApi.Services;

public interface IBooksService
{
    Task<List<Book>> GetAllAsync(Genre? genre, CancellationToken ct);
    Task<Book?> GetByIdAsync(int id, CancellationToken ct);
    Task<Book> CreateAsync(CreateBookDto createBookDto, CancellationToken ct);
    Task<bool> UpdateAsync(int id, UpdateBookDto updateBookDto, CancellationToken ct);
    Task<bool> DeleteAsync(int id, CancellationToken ct);
};