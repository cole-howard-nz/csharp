namespace Playground;

public class RecordsClassesInterfaces
{
  public record Order
  (
    int Id,
    string Item,
    int Quantity,
    decimal UnitPrice
  );

  public interface IPricer
  {
    decimal Total(Order o);
  }

  public class TaxPricer(decimal rate) : IPricer
  {
    public decimal Total(Order o) => 
      o.Quantity * o.UnitPrice * (1 + rate);   
  }

  public static void Run()
  {
    var order = new Order(Id: 1, Item: "Widget", Quantity: 3, UnitPrice: 9.99m);
    var bigger = order with { Quantity = 5 };

    Console.WriteLine(order);
    Console.WriteLine(bigger);

    IPricer pricer = new TaxPricer(rate: 0.15m);
    Console.WriteLine(pricer.Total(order));
    Console.WriteLine(pricer.Total(bigger));  
  }
}