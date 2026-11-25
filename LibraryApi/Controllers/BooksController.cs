using System.ComponentModel.DataAnnotations;
using LibraryApi.Dtos;
using LibraryApi.Mappings;
using LibraryApi.Models;
using LibraryApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController(IBooksService service) : ControllerBase
{
  [HttpGet]
  public async Task<ActionResult<List<BookDto>>> GetAll(
    [FromQuery, EnumDataType(typeof(Genre))] Genre? genre, CancellationToken ct)
  {
    List<Book> books = await service.GetAllAsync(genre, ct);
    List<BookDto> dtos = [.. books.Select(book => book.ToDto()) ];

    return Ok(dtos);
  }

  [HttpGet("{id:int}")]
  public async Task<ActionResult<BookDto>> GetById(
    [FromRoute] int id, CancellationToken ct)
  {
    Book? book = await service.GetByIdAsync(id, ct);

    return book is not null ? Ok(book.ToDto()) : NotFound("Book not found.");
  }

  [HttpPost]
  public async Task<ActionResult<BookDto>> Create(
    CreateBookDto createBookDto, CancellationToken ct)
  {
    Book book = await service.CreateAsync(createBookDto, ct);
    BookDto bookDto = book.ToDto();

    return CreatedAtAction(nameof(GetById), new { id = bookDto.Id }, bookDto);    
  }

  [HttpPut("{id:int}")]
  public async Task<ActionResult> Update(
    [FromRoute] int id, UpdateBookDto updateBookDto, CancellationToken ct)
  {
    bool success = await service.UpdateAsync(id, updateBookDto, ct);
    
    return success ? NoContent() : NotFound("Book not found.");
  }

  [HttpDelete("{id:int}")]
  public async Task<ActionResult> Delete(
    [FromRoute] int id, CancellationToken ct)
  {
    bool success = await service.DeleteAsync(id, ct);

    return success ? NoContent() : NotFound("Book not found.");
  }
}
