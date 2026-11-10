namespace Playground;

public abstract class Pricer : IPricer
{
    public abstract decimal Total(Order o);

    protected decimal Subtotal(Order o) => o.Quantity * o.UnitPrice;

    public virtual string Name => GetType().Name;
}
