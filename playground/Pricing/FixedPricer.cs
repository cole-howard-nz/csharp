namespace Playground;

public class FixedPricer : IPricer
{
    public decimal Total(Order _) => 100m;
}
