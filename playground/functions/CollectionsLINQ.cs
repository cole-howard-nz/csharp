namespace Playground;

public class CollectionsLINQ
{
  public static void Run()
  {
    var nums = new List<int> { 3, 53, 1, 45, 412, 91 };
    var topThree = nums
      .Where(num => num > 2)
      .OrderByDescending(n => n)
      .Take(3)
      .Reverse()
      .ToList();

    Console.WriteLine($"The top three numbers from the list are....");
    Console.WriteLine(string.Join(", ", topThree));

    var lookup = new Dictionary<string, int> { ["one"] = 1, ["two"] = 2, ["three"] = 3 };
    foreach (var (key, value) in lookup)
    {
      Console.WriteLine($"{key} => {value}");
    }
  }
} 