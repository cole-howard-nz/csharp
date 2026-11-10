namespace Playground;

public class ResultsDemo()
{
    public static void Run()
    {
        Order o1 = new(Id: 1, Item: "Peanut Butter", Quantity: 1, UnitPrice: 5.99m);
        Order o2 = new(Id: 2, Item: "Void Peanut Butter", Quantity: -1, UnitPrice: 5.99m);

        DiscountPricer discountPricer = new(percent: 0.1m);
        OrderService os1 = new(discountPricer);

        foreach (Order o in new[] { o1, o2 })
        {
            Result<decimal> result = os1.TryCheckout(o);

            if (result.IsSuccess)
            {
                Console.WriteLine($"Order {o.Id}: {result.Value}");
            }
            else
            {
                Console.WriteLine($"Order {o.Id}: {result.Error}");
            }
        }
    }
}
