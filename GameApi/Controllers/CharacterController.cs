using Microsoft.AspNetCore.Mvc;
using GameApi.Models;
using GameApi.Data;
using GameApi.Dtos;
using GameApi.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CharacterController(GameDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CharacterDto>>> GetAll(
        [FromQuery(Name = "class")] CharacterClass? characterClass, [FromQuery] int? minLevel, CancellationToken ct)
    {
        IQueryable<Character> characters = db.Characters;

        if (characterClass is not null)
        {
            characters = characters.Where(c => c.Class == characterClass);
        } 
        if (minLevel is not null)
        {
            characters = characters.Where(c => c.Level >= minLevel);
        }

        List<CharacterDto> result = await characters.Select(c => c.ToDto()).ToListAsync();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CharacterDto>> GetById(
        [FromRoute] int id, CancellationToken ct)
    {
        Character? character = await db.Characters
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id, ct);

        if (character is null)
        {
            return NotFound("Character not found.");
        }

        return Ok(character.ToDto());
    }

    [HttpPost]
    public async Task<ActionResult<CharacterDto>> Create(
        [FromBody] CreateCharacterDto dto, CancellationToken ct)
    {
        Character character = dto.ToEntity();
        db.Characters.Add(character);
        await db.SaveChangesAsync(ct);

        CharacterDto characterDto = character.ToDto();
        return CreatedAtAction(nameof(GetById), new { characterDto.Id }, characterDto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        [FromRoute] int id, [FromBody] UpdateCharacterDto dto, CancellationToken ct)
    {
        Character? character = await db.Characters.FirstOrDefaultAsync(c => c.Id == id);
        if (character is null)
        {
            return NotFound("Character not found.");
        }
    
        dto.ApplyTo(character);
        await db.SaveChangesAsync(ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        [FromRoute] int id, CancellationToken ct)
    {
        Character? character = await db.Characters.FirstOrDefaultAsync(c => c.Id == id, ct);

        if (character is null)
        {
            return NotFound("Character not found.");
        }

        db.Characters.Remove(character);
        await db.SaveChangesAsync(ct);

        return NoContent();
    }

    [HttpPost("{id:int}/levelup")]
    public async Task<IActionResult> LevelUp(
        [FromRoute] int id, CancellationToken ct)
    {
        Character? character = await db.Characters.FirstOrDefaultAsync(c => c.Id == id, ct);

        if (character is null)
        {
            return NotFound("Character not found.");
        }

        if (character.Level >= 99)
        {
            return Conflict(new { error = "Character is already max level." });
        }

        character.Level += 1;
        character.Hitpoints += 10;

        await db.SaveChangesAsync(ct);

        return Ok(character.ToDto());
    }
}
