namespace Playground;

public static class PatternMatchingSwitch
{
  public static void Run()
  {
    foreach (var x in new object?[] { -4, 7, "", "Hello, world!", null, 3.5, true })
    {
      Console.WriteLine(Describe(x));
    }
  }

  public static string Describe(object? o)
  {
    return o switch
    {
      int n when n < 0     => "negative int",
      int                  => "int",
      string { Length: 0 } => "empty string",
      string s             => $"string of length {s.Length}",
      null                 => "null",
      bool                 => "boolean",
      _                    => "something else"
    };
  } 
}