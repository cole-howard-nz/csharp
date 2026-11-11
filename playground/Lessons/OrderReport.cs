namespace Playground;

public static class OrderReport
{
    public static void Run()
    {
        List<Order> validOrders = [.. SampleData.Orders
            .Where(o => o.Quantity > 0 && o.UnitPrice >= 0)
        ];

        var lines = validOrders.Select(o => new
        {
            o.Id,
            o.Item,
            LineTotal = o.UnitPrice * o.Quantity
        });
        foreach (var l in lines)
        {
            Console.WriteLine($"#{l.Id} - Item: {l.Item}, Total: {l.LineTotal}");
        }

        var report = validOrders.GroupBy(o => o.Item)
            .Select(g => new {
                g.Key,
                Units = g.Sum(o => o.Quantity),
                Revenue = g.Sum(o => o.UnitPrice * o.Quantity)
            })
            .OrderByDescending(o => o.Revenue);
        foreach (var r in report)
        {
            Console.WriteLine($"{r.Key} - {r.Units} units, {r.Revenue:C}");
        }

        Order? mostValuable = SampleData.Orders.MaxBy(o => o.UnitPrice * o.Quantity);
        Console.WriteLine($"Most valuable: {mostValuable}");

        int numberOfValidOrders = validOrders.Count;
        Console.WriteLine($"Number of valid orders: {numberOfValidOrders}");

        bool doInvalidOrdersExist = SampleData.Orders
            .Any(o => o.Quantity <= 0 || o.UnitPrice < 0);
        int numberOfInvalidOrders = SampleData.Orders
            .Where(o => o.Quantity <= 0 || o.UnitPrice < 0)
            .ToList()
            .Count;
        Console.WriteLine($"Do invalid orders exist? {doInvalidOrdersExist}");
        Console.WriteLine($"Number of invalid orders: {numberOfInvalidOrders}");

        List<int> idsOfInvalidOrders = [.. SampleData.Orders
            .Where(o => o.Quantity <= 0 || o.UnitPrice < 0)
            .Select(o => o.Id)
        ];
        Console.WriteLine(string.Join(", ", idsOfInvalidOrders));

        var query = SampleData.Orders.Where(o => o.Quantity > 3);
        Order popcorn = new(Id: 11, Item: "Popcorn", Quantity: 5, UnitPrice: 4.5m);
        SampleData.Orders.Add(popcorn);
        foreach (Order o in query)
        {
            Console.WriteLine(o);
        }

        var query2 = SampleData.Orders.Where(o => o.Quantity > 3).ToList();
        Order popcorn2 = new(Id: 12, Item: "Popcorn Again", Quantity: 5, UnitPrice: 4.5m);
        SampleData.Orders.Add(popcorn2);
        foreach (Order o in query2)
        {
            Console.WriteLine(o);
        }
    }       
}
