using GameApi.Models;

namespace GameApi.Data;

public class CharacterStore
{
    private readonly List<Character> _characters = [
        new() { Id = 1, Name = "Brynn", Class = CharacterClass.Warrior, Level = 5, Hitpoints = 200, Strength = 18, Intelligence = 4, AdminNote = "playtest account", CreatedAt = DateTime.UtcNow},
        new() { Id = 2, Name = "Zeph", Class = CharacterClass.Mage, Level = 7, Hitpoints = 140, Strength = 5, Intelligence = 19, AdminNote = "top of the leaderboard", CreatedAt = DateTime.UtcNow},
        new() { Id = 3, Name = "Nyx", Class = CharacterClass.Rogue, Level = 3, Hitpoints = 120, Strength = 9, Intelligence = 8, CreatedAt = DateTime.UtcNow}
    ];

    private int _nextId = 4;
    private readonly Lock _lock = new();

    public List<Character> All()
    {
        lock (_lock) return [.. _characters];
    }

    public Character? Find(int id)
    {
        lock (_lock) return _characters.FirstOrDefault(c => c.Id == id);
    }

    public Character Add(Character character)
    {
        lock (_lock) 
        {
            character.Id = _nextId++;
            _characters.Add(character);
            return character;
        }
    }

    public bool Remove(int id)
    {
        lock (_lock)
        {
            Character? character = _characters.Find(c => c.Id == id);
            return character is not null && _characters.Remove(character);
        }
    }
}
