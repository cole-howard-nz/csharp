using LibraryApi.Models;

namespace LibraryApi.Dtos;

public record CreateBookDto(
    string Title,
    string Author,
    Genre Genre,
    int PublishedYear,
    int CopiesAvailable
);