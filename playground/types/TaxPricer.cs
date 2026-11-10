namespace Playground;

public class TaxPricer(decimal Rate) : Pricer
{
  public override decimal Total(Order o) => Subtotal(o) * (1 + Rate);
}
