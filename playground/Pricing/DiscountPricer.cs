namespace Playground;

public class DiscountPricer(decimal percent) : Pricer
{
    public override decimal Total(Order o) => Subtotal(o) * (1 - percent);

    public override string Name => $"Discount {percent:P0}";
}
