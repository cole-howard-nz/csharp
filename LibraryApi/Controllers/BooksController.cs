using System.ComponentModel.DataAnnotations;
using LibraryApi.Dtos;
using LibraryApi.Mappings;
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
  public ActionResult<List<BookDto>> GetAll(
    [FromQuery, EnumDataType(typeof(Genre))] Genre? genre)
  {
    List<Book> result = books;
    if (genre is not null)
    {
        result = [.. books.Where(book => book.Genre == genre)];
    }

    List<BookDto> dtos = [.. result.Select(book => book.ToDto())];
    return Ok(dtos);
  }

  [HttpGet("{id:int}")]
  public ActionResult<BookDto> GetById(
    [FromRoute] int id)
  {
    Book? book = books.FirstOrDefault(book => book.Id == id);
    return book is not null ? Ok(book.ToDto()) : NotFound("Book not found."); 
  }

  [HttpPost()]
  public ActionResult<BookDto> Create(
    CreateBookDto createBookDto)
  {
    Book book = createBookDto.ToEntity();
    book.Id = books.Count == 0 ? 1 : books.Max(book => book.Id) + 1;
    books.Add(book);

    BookDto bookDto = book.ToDto();
    return CreatedAtAction(nameof(GetById), new { id = bookDto.Id }, bookDto);    
  }

  [HttpPut("{id:int}")]
  public ActionResult Update(
    [FromRoute] int id, UpdateBookDto updateBookDto)
  {
    Book? book = books.FirstOrDefault(book => book.Id == id);

    if (book is null)
    {
      return NotFound("Book not found.");
    }

    updateBookDto.ApplyTo(book);

    return NoContent();
  }

  [HttpDelete("{id:int}")]
  public ActionResult Delete(
    [FromRoute] int id)
  {
    Book? book = books.FirstOrDefault(b => b.Id == id);

    if (book is null)
    {
      return NotFound("Book not found.");
    }

    books.Remove(book);

    return NoContent();
  }
}
