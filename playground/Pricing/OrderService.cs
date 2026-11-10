namespace Playground;

public class OrderService(IPricer pricer)
{
    public decimal Checkout(Order o) => Math.Round(pricer.Total(o), 2);
}
