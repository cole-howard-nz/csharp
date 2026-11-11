namespace Playground;

public static class PricerFactory
{
    public static IPricer For(CustomerTier tier) => tier switch
    {
        CustomerTier.Standard => new DiscountPricer(percent: 0.15m),
        CustomerTier.Silver => new DiscountPricer(percent: 0.05m),
        CustomerTier.Gold => new DiscountPricer(percent: 0.10m),
        _ => throw new ArgumentOutOfRangeException(nameof(tier))
    };

    public static IPricer For(Customer c, Order o) => (c.Tier, o.Quantity) switch
    {
        (CustomerTier.Gold, >= 10) => new DiscountPricer(percent: 0.20m),
        _ => For(c.Tier)
    };
}
