namespace Playground;

public static class AsyncStuff
{
  public static async void Run()
  {
    string label = "I am the label. Fear me";
    Console.WriteLine(await FetchAsync(label));

    var stopwatch = System.Diagnostics.Stopwatch.StartNew();
    var results = await Task.WhenAll(
      FetchAsync("A"),
      FetchAsync("B"),
      FetchAsync("C")
    );
    Console.WriteLine($"{string.Join(" | ", results)} in {stopwatch.ElapsedMilliseconds}ms");
  }

  public static async Task<string> FetchAsync(string label)
  {
    await Task.Delay(millisecondsDelay: 500);
    return $"{label}: done";
  }
}
