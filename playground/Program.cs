namespace Playground;

public class Program
{
  public static async Task Main()
  {
    DoBasics();
    DoCollectionsLINQ();
    DoRecordsClassesInterfaces();
    await DoAsyncStuff();
    DoPatternMatchingSwitch();
  }

  public static void DoBasics() => Basics.Run();
  public static void DoCollectionsLINQ() => CollectionsLINQ.Run();
  public static void DoRecordsClassesInterfaces() => RecordsClassesInterfaces.Run();
  public static void DoPatternMatchingSwitch() => PatternMatchingSwitch.Run();

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