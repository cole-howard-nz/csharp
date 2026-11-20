using Microsoft.AspNetCore.Mvc;
using GameApi.Models;
using GameApi.Dtos;
using GameApi.Mapping;
using Microsoft.EntityFrameworkCore;
using GameApi.Services;

namespace GameApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CharacterController(ICharacterService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CharacterDto>>> GetAll(
        [FromQuery(Name = "class")] CharacterClass? characterClass, [FromQuery] int? minLevel, CancellationToken ct)
    {
        List<Character> characters = await service.GetAllAsync(characterClass, minLevel, ct);

        if (characterClass is not null)
        {
            characters = [.. characters.Where(c => c.Class == characterClass)];
        } 
        if (minLevel is not null)
        {
            characters = [.. characters.Where(c => c.Level >= minLevel)];
        }

        List<CharacterDto> result = [.. characters.Select(c => c.ToDto())];
        
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CharacterDto>> GetById(
        [FromRoute] int id, CancellationToken ct)
    {
        Character? character = await service.GetByIdAsync(id, ct);

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
        Character result = await service.CreateAsync(character, ct);
        CharacterDto characterDto = result.ToDto();

        return CreatedAtAction(nameof(GetById), new { characterDto.Id }, characterDto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        [FromRoute] int id, [FromBody] UpdateCharacterDto dto, CancellationToken ct)
    {
        bool success = await service.UpdateAsync(id, dto, ct);

        if (!success)
        {
            return NotFound("Character not found.");
        }
    
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        [FromRoute] int id, CancellationToken ct)
    {
        bool success = await service.DeleteAsync(id, ct);

        if (!success)
        {
            return NotFound("Character not found.");
        }

        return NoContent();
    }

    [HttpPost("{id:int}/levelup")]
    public async Task<IActionResult> LevelUp(
        [FromRoute] int id, CancellationToken ct)
    {
        (LevelUpResult Result, Character? Character) character = await service.LevelUpAsync(id, ct);

        if (character.Result is LevelUpResult.NotFound || character.Character is null)
        {
            return NotFound("Character not found.");
        }

        if (character.Result is LevelUpResult.MaxLevel)
        {
            return Conflict(new { error = "Character is already max level." });
        }

        character.Character.Level += 1;
        character.Character.Hitpoints += 10;

        return Ok(character.Character.ToDto());
    }
}
