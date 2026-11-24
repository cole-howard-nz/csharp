using LibraryApi.Dtos;
using LibraryApi.Models;

namespace LibraryApi.Mappings;

public static class BookMapping
{
    public static BookDto ToDto(this Book book)
    {
        return new(
            Id: book.Id,
            Title: book.Title,
            Author: book.Author,
            Genre: book.Genre,
            PublishedYear: book.PublishedYear,
            CopiesAvailable: book.CopiesAvailable,
            AddedAt: book.AddedAt,
            IsAvailable: book.CopiesAvailable > 0
        );
    }

    public static Book ToEntity(this CreateBookDto dto)
    {
        return new() {
            Title = dto.Title,
            Author = dto.Author,
            Genre = dto.Genre, 
            PublishedYear = dto.PublishedYear,
            CopiesAvailable = dto.CopiesAvailable,
            AddedAt = DateTime.UtcNow
        };
    }

    public static void ApplyTo(this UpdateBookDto dto, Book book)
    {
        book.Title = dto.Title;
        book.Author = dto.Author;
        book.CopiesAvailable = dto.CopiesAvailable;
    }
}