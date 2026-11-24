using LibraryApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController() : ControllerBase
{
  public static List<Book> books = [
    new() { Id=1, Title="The Hobbit", Author="J.R.R Tolkien", Genre=Genre.Fantasy, PublishedYear=1937, CopiesAvailable=3, AddedAt=new DateTime(2026, 01, 01, 0, 0, 0, DateTimeKind.Utc), ShelfNote="Fragile spine" },
    new() { Id=2, Title="A Brief History of Time", Author="Stephen Hawking", Genre=Genre.Science, PublishedYear=1988, CopiesAvailable=0, AddedAt=new DateTime(2026, 01, 02, 0, 0, 0, DateTimeKind.Utc) },
    new() { Id=3, Title="Sapiens", Author="Yuval Noah Harari", Genre=Genre.History, PublishedYear=2011, CopiesAvailable=2, AddedAt=new DateTime(2026, 01, 03, 0, 0, 0, DateTimeKind.Utc), ShelfNote="Display copy" }
  ];

  
  [HttpGet()]
  public ActionResult<List<Book>> GetAll()
  {
    return Ok(books);
  }

  [HttpGet("{id:int}")]
  public ActionResult<Book> GetById(
    [FromRoute] int id)
  {
    Book? book = books.FirstOrDefault(book => book.Id == id);
    return book is not null ? Ok(book) : NotFound("Book not found."); 
  }

  [HttpPost()]
  public ActionResult<Book> Create(
    Book book)
  {
    books.Add(book);
    return CreatedAtAction(nameof(GetById), new { book.Id }, book);    
  }
}