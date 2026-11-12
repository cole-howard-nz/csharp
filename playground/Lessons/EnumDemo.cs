namespace Playground;

public static class EnumDemo
{
    public static async Task Run()
    {
        Order o1 = new(Id: 1, Item: "Peanut Butter", Quantity: 1, UnitPrice: 5.99m);
        Order o2  = new(Id: 2, Item: "Peanut Butter", Quantity: 10, UnitPrice: 5.99m);

        List<Customer> customers =
        [
            new() { Name = "Ana",  Tier = CustomerTier.Standard },
            new() { Name = "Ben",  Tier = CustomerTier.Silver },
            new() { Name = "Cleo", Tier = CustomerTier.Gold },
        ];

        foreach (Customer c in customers)
        {
            IPricer p1 = PricerFactory.For(c, o1);
            OrderService os1 = new(p1);
            Console.WriteLine($"{c.Name} ({c.Tier}) {await os1.TryCheckoutAsync(o1, CancellationToken.None)}");

            IPricer p2 = PricerFactory.For(c, o2);
            OrderService os2 = new(p2);
            Console.WriteLine($"{c.Name} ({c.Tier}) {await os2.TryCheckoutAsync(o2, CancellationToken.None)}");
        }
    }
}
