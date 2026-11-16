using Microsoft.AspNetCore.Mvc;
using GameApi.Models;
using GameApi.Data;
using GameApi.Dtos;
using GameApi.Mapping;

namespace GameApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CharacterController(CharacterStore store) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<CharacterDto>> GetAll([FromQuery(Name = "class")] CharacterClass? characterClass, [FromQuery] int? minLevel)
    {
        IEnumerable<Character> characters = store.All();

        if (characterClass is not null)
        {
            characters = characters.Where(c => c.Class == characterClass);
        } 
        if (minLevel is not null)
        {
            characters = characters.Where(c => c.Level >= minLevel);
        }

        IEnumerable<CharacterDto> result = characters.Select(c => c.ToDto());
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public ActionResult<CharacterDto> GetById([FromRoute] int id)
    {
        Character? character = store.Find(id);

        if (character == null)
        {
            return NotFound("Character not found.");
        }

        return Ok(character.ToDto());
    }

    [HttpPost]
    public ActionResult<CharacterDto> Create([FromBody] CreateCharacterDto dto)
    {
        Character character = store.Add(dto.ToEntity());
        CharacterDto characterDto = character.ToDto();
        return CreatedAtAction(nameof(GetById), new { characterDto.Id }, characterDto);
    }

    [HttpPut("{id:int}")]
    public IActionResult Update([FromRoute] int id, [FromBody] UpdateCharacterDto dto)
    {
        Character? character = store.Find(id);
        if (character is null)
        {
            return NotFound("Character not found.");
        }
    
        dto.ApplyTo(character);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete([FromRoute] int id)
    {
        bool result = store.Remove(id);

        if (!result)
        {
            return NotFound("Character not found.");
        }

        return NoContent();
    }

    [HttpPost("{id:int}/levelup")]
    public IActionResult LevelUp([FromRoute] int id)
    {
        Character? character = store.Find(id);

        if (character is null)
        {
            return NotFound("Character not found.");
        }

        if (character.Level == 99)
        {
            return Conflict(new { error = "Character is already max level." });
        }

        character.Level += 1;
        character.Hitpoints += 10;

        return Ok(character.ToDto());
    }
}
