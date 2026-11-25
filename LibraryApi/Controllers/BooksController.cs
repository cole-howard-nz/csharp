using System.ComponentModel.DataAnnotations;
using LibraryApi.Data;
using LibraryApi.Dtos;
using LibraryApi.Mappings;
using LibraryApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController(LibraryDbContext db) : ControllerBase
{
  [HttpGet]
  public async Task<ActionResult<List<BookDto>>> GetAll(
    [FromQuery, EnumDataType(typeof(Genre))] Genre? genre, CancellationToken ct)
  {
    IQueryable<Book> query = db.Books.AsNoTracking(); 
    if (genre is not null)
    {
        query = query.Where(book => book.Genre == genre);
    }

    List<Book> books = await query.OrderBy(book => book.Id).ToListAsync(ct);
    List<BookDto> dtos = [.. books.Select(book => book.ToDto()) ];
    return Ok(dtos);
  }

  [HttpGet("{id:int}")]
  public async Task<ActionResult<BookDto>> GetById(
    [FromRoute] int id, CancellationToken ct)
  {
    Book? book = await db.Books.AsNoTracking().FirstOrDefaultAsync(book => book.Id == id, ct);
    return book is not null ? Ok(book.ToDto()) : NotFound("Book not found."); 
  }

  [HttpPost]
  public async Task<ActionResult<BookDto>> Create(
    CreateBookDto createBookDto, CancellationToken ct)
  {
    Book book = createBookDto.ToEntity();
    db.Books.Add(book);
    await db.SaveChangesAsync(ct);

    BookDto bookDto = book.ToDto();
    return CreatedAtAction(nameof(GetById), new { id = bookDto.Id }, bookDto);    
  }

  [HttpPut("{id:int}")]
  public async Task<ActionResult> Update(
    [FromRoute] int id, UpdateBookDto updateBookDto, CancellationToken ct)
  {
    Book? book = await db.Books.FirstOrDefaultAsync(book => book.Id == id, ct);

    if (book is null)
    {
      return NotFound("Book not found.");
    }

    updateBookDto.ApplyTo(book);
    await db.SaveChangesAsync(ct);

    return NoContent();
  }

  [HttpDelete("{id:int}")]
  public async Task<ActionResult> Delete(
    [FromRoute] int id, CancellationToken ct)
  {
    Book? book = await db.Books.FirstOrDefaultAsync(book => book.Id == id, ct);

    if (book is null)
    {
      return NotFound("Book not found.");
    }

    db.Books.Remove(book);
    await db.SaveChangesAsync(ct);

    return NoContent();
  }
}
