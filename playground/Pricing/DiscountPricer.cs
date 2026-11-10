namespace Playground;

public class DiscountPricer(decimal Percent) : Pricer
{
    public override decimal Total(Order o) => Subtotal(o) * (1 - Percent);

    public override string Name => $"Discount {Percent:P0}";
}
