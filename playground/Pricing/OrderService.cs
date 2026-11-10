namespace Playground;

public class OrderService(IPricer pricer)
{
    public decimal Checkout(Order o) => Math.Round(pricer.Total(o), 2);
    public Result<decimal> TryCheckout(Order o)
    {
        try
        {
            decimal checkout = Checkout(o);
            return Result<decimal>.Ok(checkout);
        } catch (InvalidOrderException ex)
        {
            return Result<decimal>.Fail(ex.Message);
        }
    }
}
