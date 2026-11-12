namespace Playground;

public static class AsyncPricing
{
    public static async Task Run()
    {
        List<Order> valid = [.. SampleData.Orders
            .Where(o => o.Quantity > 0 && o.UnitPrice >= 0)
        ];
        OrderService orderService = new(pricer: new SlowPricer());

        Console.WriteLine($"Valid count: {valid.Count()}");

        // Sequentially, slow.
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        foreach (Order o in valid)
        {
            await orderService.TryCheckoutAsync(o, CancellationToken.None);
        }
        Console.WriteLine($"Sequential time: {stopwatch.ElapsedMilliseconds}ms");

        // Concurrent, fast!
        stopwatch.Restart();
        List<Task<decimal>> tasks = [.. valid
            .Select(o => orderService.CheckoutAsync(o, CancellationToken.None))
        ];
        await Task.WhenAll(tasks);
        Console.WriteLine($"Concurrent time: {stopwatch.ElapsedMilliseconds}ms");
        stopwatch.Stop();

        using var cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(100));
        try
        {
            await orderService.CheckoutAsync(valid.First(), cts.Token);
            Console.WriteLine("Finished, this should never print");
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Operation cancelled, took too long");
        }
    }
}
