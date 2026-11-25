using System.ComponentModel.DataAnnotations;
using LibraryApi.Models;

namespace LibraryApi.Dtos;

public record CreateBookDto(
    [StringLength(200)]
    string Title,

    [StringLength(100)]
    string Author,
    
    Genre Genre,
    int PublishedYear,
    
    [Range(0, int.MaxValue)]
    int CopiesAvailable
);