namespace Playground;

public static class Exceptions
{
    public static void Run()
    {
        Order o1 = new(Id: 1, Item: "Peanut Butter", Quantity: 1, UnitPrice: 5.99m);
        Order o2 = new(Id: 2, Item: "Void Peanut Butter", Quantity: -1, UnitPrice: 5.99m);
        Order o3 = new(Id: 3, Item: "Free Peanut Butter", Quantity: 1, UnitPrice: -2.50m);
        Order o4 = new(Id: 4, Item: "Free Peanut Butter", Quantity: 0, UnitPrice: 5.99m);

        List<Order> list = new List<Order> { o1, o2, o3, o4 };

        try
        {
            foreach (Order o in list)
            {
                DiscountPricer discountPricer = new(Percent: 0.1m);
                TaxPricer taxPricer = new(Rate: 0.2m);

                Console.WriteLine($"Total from discount pricer is ${discountPricer.Total(o)}");
                Console.WriteLine($"Total from tax pricer is ${taxPricer.Total(o)}");
            }
        }
        catch (InvalidOrderException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (Exception ex) when (ex is ArgumentException)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            Console.WriteLine("Pricing attempt finished");
        }
    }
}
