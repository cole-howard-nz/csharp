namespace Playground;

public static class DependencyInjection
{
    public async static void Run()
    {
        TaxPricer taxPricer = new(rate: 0.1m);
        DiscountPricer discountPricer = new(percent: 0.2m);
        FixedPricer fixedPricer = new();

        Order o = new(Id: 1, Item: "Peanut Butter", Quantity: 1, UnitPrice: 5.99m);
        OrderService os1 = new(pricer: taxPricer);
        OrderService os2 = new(pricer: discountPricer);
        OrderService os3 = new(pricer: fixedPricer);

        Console.WriteLine(await os1.CheckoutAsync(o, CancellationToken.None));
        Console.WriteLine(await os2.CheckoutAsync(o, CancellationToken.None));
        Console.WriteLine(await os3.CheckoutAsync(o, CancellationToken.None));
    }
}
