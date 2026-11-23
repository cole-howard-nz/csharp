using GameApi.Data;
using GameApi.Dtos;
using GameApi.Models;
using GameApi.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameApi.Services;

public class CharacterService(GameDbContext db) : ICharacterService
{
    public async Task<List<Character>> GetAllAsync(
        CharacterClass? characterClass, int? minLevel, CancellationToken ct)
    {
        IQueryable<Character> characters = db.Characters.AsNoTracking();

        if (characterClass is not null)
        {
            characters = characters.Where(c => c.Class == characterClass);
        } 
        if (minLevel is not null)
        {
            characters = characters.Where(c => c.Level >= minLevel);
        }

        List<Character> result = await characters.OrderBy(c => c.Id).ToListAsync(ct);
        return result;
    }

    public async Task<Character?> GetByIdAsync(
        int id, CancellationToken ct)
    {
        return await db.Characters
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id, ct);
    }

    public async Task<Character> CreateAsync(
        Character character, CancellationToken ct)
    {
        db.Characters.Add(character);
        await db.SaveChangesAsync(ct);

        return character;
    }

    public async Task<bool> UpdateAsync(
        int id, UpdateCharacterDto dto, CancellationToken ct)
    {
        Character? character = await db.Characters.FirstOrDefaultAsync(c => c.Id == id, ct);
        if (character is null)
        {
            return false;
        }
    
        dto.ApplyTo(character);
        await db.SaveChangesAsync(ct);

        return true;
    }

    public async Task<bool> DeleteAsync(
        int id, CancellationToken ct)
    {
        Character? character = await db.Characters.FirstOrDefaultAsync(c => c.Id == id, ct);

        if (character is null)
        {
            return false;
        }

        db.Characters.Remove(character);
        await db.SaveChangesAsync(ct);

        return true;
    }

    public async Task<(LevelUpResult Result, Character? Character)> LevelUpAsync(
        int id, CancellationToken ct)
    {
        Character? character = await db.Characters.FirstOrDefaultAsync(c => c.Id == id, ct);

        if (character is null)
        {
            return (LevelUpResult.NotFound, null);
        }

        if (character.Level >= 99)
        {
            return (LevelUpResult.MaxLevel, null);
        }

        character.Level += 1;
        character.Hitpoints += 10;

        await db.SaveChangesAsync(ct);

        return (LevelUpResult.Ok, character);
    }
}
