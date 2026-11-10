namespace Playground;

public static class AsyncStuff
{
  public static async Task<string> FetchAsync(string label)
  {
    await Task.Delay(millisecondsDelay: 500);
    return $"{label}: done";
  }
}
