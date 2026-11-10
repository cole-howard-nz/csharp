namespace Playground;

public static class Inheritance
{
    public static void Run()
    {
        Order o = new(Id: 1, Item: "Peanut Butter", Quantity: 1, UnitPrice: 5.99m);

        DiscountPricer discountPricer = new(percent: 0.1m);
        Console.WriteLine($"{discountPricer.Name} - {discountPricer.Total(o)}");

        TaxPricer taxPricer = new(rate: 0.2m);
        Console.WriteLine($"{taxPricer.Name} - {taxPricer.Total(o)}");
    }
}
