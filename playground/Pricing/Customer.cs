namespace Playground;

public class Customer
{
    public required string Name { get; init; } 
    public CustomerTier Tier { get; init; } = CustomerTier.Standard;
}
