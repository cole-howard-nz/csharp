namespace Playground;

public class TaxPricer(decimal rate) : IPricer
{
  public decimal Total(Order o) => 
    o.Quantity * o.UnitPrice * (1 + rate);   
}