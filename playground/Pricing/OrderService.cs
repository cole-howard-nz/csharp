namespace Playground;

public class OrderService(IPricer pricer)
{
    public async Task<decimal> CheckoutAsync(Order o, CancellationToken ct)
    {
        decimal total = await pricer.TotalAsync(o, ct);
        return Math.Round(total, 2, MidpointRounding.AwayFromZero);
    }

    public async Task<Result<decimal>> TryCheckoutAsync(Order o, CancellationToken ct)
    {
        try
        {
            return Result<decimal>.Ok(await CheckoutAsync(o, ct));
        } 
        catch (InvalidOrderException ex)
        {
            return Result<decimal>.Fail(ex.Message);
        }
    }
}
