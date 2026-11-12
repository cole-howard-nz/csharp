namespace Playground;

public class SlowPricer : IPricer
{
    public async Task<decimal> TotalAsync(Order o, CancellationToken ct)
    {
        await Task.Delay(300, ct);
        return o.Quantity * 10m;
    }
}
