using LibraryApi.Models;

namespace LibraryApi.Dtos;

public record BookDto(
    int Id,
    string Title,
    string Author,
    Genre Genre,
    int PublishedYear,
    int CopiesAvailable,
    DateTime AddedAt,
    bool IsAvailable
);