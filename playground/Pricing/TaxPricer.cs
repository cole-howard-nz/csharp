namespace Playground;

public class TaxPricer(decimal rate) : Pricer
{
  public override Task<decimal> TotalAsync(Order o, CancellationToken ct) => Task.FromResult(Subtotal(o) * (1 + rate));
}
