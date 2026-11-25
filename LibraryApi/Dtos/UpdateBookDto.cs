using System.ComponentModel.DataAnnotations;

namespace LibraryApi.Dtos;

public record UpdateBookDto(
    [StringLength(200)]
    string Title,

    [StringLength(100)]
    string Author,

    [Range(0, int.MaxValue)]
    int CopiesAvailable
);