using GameApi.Dtos;
using GameApi.Models;

namespace GameApi.Services;

public enum LevelUpResult
{
    Ok,
    NotFound,
    MaxLevel
}

public interface ICharacterService
{
    Task<List<Character>> GetAllAsync(CharacterClass? characterClass, int? minLevel, CancellationToken ct);
    Task<Character?> GetByIdAsync(int id, CancellationToken ct);
    Task<Character> CreateAsync(Character character, CancellationToken ct);
    Task<bool> UpdateAsync(int id, UpdateCharacterDto dto, CancellationToken ct);
    Task<bool> DeleteAsync(int id, CancellationToken ct);
    Task<(LevelUpResult Result, Character? Character)> LevelUpAsync(int id, CancellationToken ct);
}
