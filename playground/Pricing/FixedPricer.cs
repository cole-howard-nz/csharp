namespace Playground;

public class FixedPricer : IPricer
{
    public Task<decimal> TotalAsync(Order _, CancellationToken ct) => Task.FromResult(100m);
}
