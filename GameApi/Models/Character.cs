namespace GameApi.Models;

public class Character
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public CharacterClass Class { get; set; }
    public int Level { get; set; }
    public int Hitpoints { get; set; }
    public int Strength { get; set; }
    public int Intelligence { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? AdminNote { get; set; }

}
