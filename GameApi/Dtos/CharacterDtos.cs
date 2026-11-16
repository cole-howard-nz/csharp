using System.ComponentModel.DataAnnotations;
using GameApi.Models;

namespace GameApi.Dtos;

public record CharacterDto(
    int Id,
    string Name,
    CharacterClass Class,
    int Level,
    int Hitpoints,
    int Strength,
    int Intelligence,
    int Power
);

public record CreateCharacterDto(
    [Required, StringLength(20, MinimumLength = 3)] 
    string Name,

    [EnumDataType(typeof(CharacterClass))]              
    CharacterClass Class,

    [Range(1, 20)] 
    int Strength,

    [Range(1, 20)] 
    int Intelligence
);

public record UpdateCharacterDto(
    [Required, StringLength(20, MinimumLength = 3)] 
    string Name,

    [Required, Range(1, 20)] 
    int Strength,

    [Required, Range(1, 20)] 
    int Intelligence
);
