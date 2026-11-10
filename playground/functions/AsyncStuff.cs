namespace Playground;

public class AsyncStuff
{
  public async static Task<string> FetchAsync(string label)
  {
    await Task.Delay(millisecondsDelay: 500);
    return $"{label}: done";
  }
}
