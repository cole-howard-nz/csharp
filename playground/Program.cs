namespace Playground;

public class Program
{
  public static async Task Main()
  {
    Basics.Run();
    CollectionsLINQ.Run();
    RecordsClassesInterfaces.Run();
    await DoAsyncStuff();
    PatternMatchingSwitch.Run();
    Inheritance.Run();
    Exceptions.Run();
    DependencyInjection.Run();
  }

  public static async Task DoAsyncStuff()
  {
    string label = "I am the label. Fear me";
    Console.WriteLine(await AsyncStuff.FetchAsync(label));

    var stopwatch = System.Diagnostics.Stopwatch.StartNew();
    var results = await Task.WhenAll(
      AsyncStuff.FetchAsync("A"),
      AsyncStuff.FetchAsync("B"),
      AsyncStuff.FetchAsync("C")
    );
    Console.WriteLine($"{string.Join(" | ", results)} in {stopwatch.ElapsedMilliseconds}ms");
  }
}
