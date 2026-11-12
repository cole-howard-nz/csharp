namespace Playground;

public class DiscountPricer(decimal percent) : Pricer
{
    public override Task<decimal> TotalAsync(Order o, CancellationToken ct) => Task.FromResult(Subtotal(o) * (1 - percent));

    public override string Name => $"Discount {percent:P0}";
}
