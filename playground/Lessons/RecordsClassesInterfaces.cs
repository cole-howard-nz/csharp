namespace Playground;

public static class RecordsClassesInterfaces
{
  public static async Task Run()
  {
    var order = new Order(Id: 1, Item: "Widget", Quantity: 3, UnitPrice: 9.99m);
    var bigger = order with { Quantity = 5 };

    Console.WriteLine(order);
    Console.WriteLine(bigger);

    IPricer pricer = new TaxPricer(rate: 0.15m);
    Console.WriteLine(await pricer.TotalAsync(order, CancellationToken.None));
    Console.WriteLine(await pricer.TotalAsync(bigger, CancellationToken.None));  
  }
}
