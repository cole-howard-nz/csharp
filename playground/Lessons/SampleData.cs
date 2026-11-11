namespace Playground;

public static class SampleData
{
    public static List<Order> Orders { get; } =
    [
        new(1, "Peanut Butter", 2, 5.99m),
        new(2, "Jam", 1, 4.50m),
        new(3, "Peanut Butter", 1, 5.99m),
        new(4, "Bread", 3, 3.20m),
        new(5, "Jam", 4, 4.50m),
        new(6, "Bread", 0, 3.20m),     // invalid: zero quantity
        new(7, "Honey", 1, 12.00m),
        new(8, "Honey", 2, -1.00m),    // invalid: negative price
        new(9, "Peanut Butter", 5, 5.99m),
        new(10, "Bread", 1, 3.20m),
    ];
}
