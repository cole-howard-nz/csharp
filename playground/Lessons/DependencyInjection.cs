namespace Playground;

public static class DependencyInjection
{
    public static void Run()
    {
        TaxPricer taxPricer = new(rate: 0.1m);
        DiscountPricer discountPricer = new(percent: 0.2m);
        FixedPricer fixedPricer = new();

        Order o = new(Id: 1, Item: "Peanut Butter", Quantity: 1, UnitPrice: 5.99m);
        OrderService os1 = new(pricer: taxPricer);
        OrderService os2 = new(pricer: discountPricer);
        OrderService os3 = new(pricer: fixedPricer);

        Console.WriteLine(os1.Checkout(o));
        Console.WriteLine(os2.Checkout(o));
        Console.WriteLine(os3.Checkout(o));
    }
}
