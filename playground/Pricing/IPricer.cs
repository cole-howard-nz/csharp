namespace Playground;

public interface IPricer
{
  Task<decimal> TotalAsync(Order o, CancellationToken ct);
}
