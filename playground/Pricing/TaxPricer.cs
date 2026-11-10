namespace Playground;

public class TaxPricer(decimal rate) : Pricer
{
  public override decimal Total(Order o) => Subtotal(o) * (1 + rate);
}
