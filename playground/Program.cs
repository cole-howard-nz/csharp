Console.WriteLine("Hello from C# 14 on .NET 10");

string name = "Cole";
int? age = null;
string? nickname = "Coala";
string greeting = $"Hi {name}, age: {age?.ToString() ?? "unknown"}";

Console.WriteLine(greeting);

if (nickname?.Length > 0)
{
  Console.WriteLine($"Your nickname is: {nickname} (length: {nickname?.Length})");
} 
else
{
  Console.WriteLine("No nickname available");
}


// -----


var nums = new List<int> { 3, 53, 1, 45, 412, 91 };
var topThree = nums
                .Where(num => num > 2)
                .OrderByDescending(n => n)
                .Take(3)
                .Reverse()
                .ToList();

Console.WriteLine($"The top three numbers from the list are....");
Console.WriteLine(string.Join(", ", topThree));

var lookup = new Dictionary<string, int> { ["one"] = 1, ["two"] = 2, ["three"] = 3 };
foreach (var (key, value) in lookup)
{
  Console.WriteLine($"{key} => {value}");
}


// -----


var order = new Order(Id: 1, Item: "Widget", Quantity: 3, UnitPrice: 9.99m);
var bigger = order with { Quantity = 5 };

Console.WriteLine(order);
Console.WriteLine(bigger);

IPricer pricer = new TaxPricer(rate: 0.15m);
Console.WriteLine(pricer.Total(order));
Console.WriteLine(pricer.Total(bigger));

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