namespace Playground;

public static class DependencyInjection
{
    public async static void Run()
    {
        Order o = new(Id: 1, Item: "Peanut Butter", Quantity: 1, UnitPrice: 5.99m);
        
        OrderService os1 = new(pricer: new TaxPricer(rate: 0.1m));
        OrderService os2 = new(pricer: new DiscountPricer(percent: 0.2m));
        OrderService os3 = new(pricer: new FixedPricer());

        Console.WriteLine(await os1.CheckoutAsync(o, CancellationToken.None));
        Console.WriteLine(await os2.CheckoutAsync(o, CancellationToken.None));
        Console.WriteLine(await os3.CheckoutAsync(o, CancellationToken.None));
    }
}
