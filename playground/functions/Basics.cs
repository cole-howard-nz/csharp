namespace Playground;

public class Basics
{
  public static void Run()
  {
    Console.WriteLine("Hello from C# 14 on .NET 10");

    string name = "Cole";
    int? age = null;
    string? nickname = "Coala";
    string greeting = $"Hi {name}, age: {age?.ToString() ?? "unknown"}";

    Console.WriteLine(greeting);

    if (nickname?.Length > 0)
    {
      Console.WriteLine($"Your nickname is: {nickname} (length: {nickname?.Length})");
    } 
    else
    {
      Console.WriteLine("No nickname available");
    }
  }
}