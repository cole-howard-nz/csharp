namespace Playground;

public abstract class Pricer : IPricer
{
    public abstract decimal Total(Order o);
    public virtual string Name => GetType().Name;
    
    protected decimal Subtotal(Order o)
    {
        Validate(o);
        return o.Quantity * o.UnitPrice;
    }

    protected void Validate(Order o)
    {
        if (o.Quantity <= 0 || o.UnitPrice < 0)
        {
            throw new InvalidOrderException(Message: $"Pricer.Validate failed: {o.Id}");
        }
    }
}
