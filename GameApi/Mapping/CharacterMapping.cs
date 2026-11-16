using GameApi.Dtos;
using GameApi.Models;

namespace GameApi.Mapping;

public static class CharacterMapping
{
    public static CharacterDto ToDto(this Character character)
    {
        return new(
            Id: character.Id,
            Name: character.Name,
            Class: character.Class,
            Level: character.Level,
            Hitpoints: character.Hitpoints,
            Strength: character.Strength,
            Intelligence: character.Intelligence,
            Power: character.Level * (character.Strength + character.Intelligence)
        );
    }

    public static Character ToEntity(this CreateCharacterDto dto)
    {
        return new()
        {
            Name = dto.Name,
            Class = dto.Class,
            Strength = dto.Strength,
            Intelligence = dto.Intelligence,
            Level = 1,
            Hitpoints = 100,
            CreatedAt = DateTime.UtcNow,
        };
    }

    public static void ApplyTo(this UpdateCharacterDto dto, Character character)
    {
        character.Name = dto.Name;
        character.Strength = dto.Strength;
        character.Intelligence = dto.Intelligence;
    }
}
