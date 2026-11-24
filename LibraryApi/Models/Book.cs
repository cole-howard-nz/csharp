namespace LibraryApi.Models;

public class Book
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Author { get; set; }
    public Genre Genre { get; set; }
    public int PublishedYear { get; set; }
    public int CopiesAvailable { get; set; }
    public DateTime AddedAt { get; set; }
    public string? ShelfNote { get; set; } 
}