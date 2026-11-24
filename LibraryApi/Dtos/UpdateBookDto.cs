namespace LibraryApi.Dtos;

public record UpdateBookDto(
    string Title,
    string Author,
    int CopiesAvailable
);