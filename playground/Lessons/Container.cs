using System.Data.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Playground;

public static class Container
{
    public static async Task Run()
    {
        ServiceCollection serviceCollection = new();

        serviceCollection.AddSingleton<IPricer>(new DiscountPricer(0.10m));
        serviceCollection.AddTransient<OrderService>();

        using ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
        OrderService orderService = serviceProvider.GetRequiredService<OrderService>();

        Order order = new(Id: 1, Item: "Peanut Butter", Quantity: 1, UnitPrice: 5.99m);
        Console.WriteLine(await orderService.CheckoutAsync(order, CancellationToken.None));

        ShowLifetimes();
    }

    private static void Compare(string label, Action<IServiceCollection> register)
    {
        ServiceCollection serviceCollection = new();
        serviceCollection.AddSingleton<IPricer>(new FixedPricer());
        register(serviceCollection);

        using ServiceProvider provider = serviceCollection.BuildServiceProvider();
        OrderService os1 = provider.GetRequiredService<OrderService>();
        OrderService os2 = provider.GetRequiredService<OrderService>();

        Console.WriteLine($"{label}: same instance? {ReferenceEquals(os1, os2)}");
    }

    private static void ShowLifetimes()
    {
        Compare("Transient", s => s.AddTransient<OrderService>());
        Compare("Singleton", s => s.AddSingleton<OrderService>());

        ServiceCollection serviceCollection = new();
        serviceCollection.AddSingleton<IPricer>(new FixedPricer());
        serviceCollection.AddScoped<OrderService>();

        using ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
        using (IServiceScope scope = serviceProvider.CreateScope())
        {
            OrderService os1 = scope.ServiceProvider.GetRequiredService<OrderService>();
            OrderService os2 = scope.ServiceProvider.GetRequiredService<OrderService>();
            Console.WriteLine($"Scoped, same scope: same instance? {ReferenceEquals(os1, os2)}");
        }

        OrderService first, second;
        using IServiceScope scopeA = serviceProvider.CreateScope();
        first = scopeA.ServiceProvider.GetRequiredService<OrderService>();

        using IServiceScope scopeB = serviceProvider.CreateScope();
        second = scopeB.ServiceProvider.GetRequiredService<OrderService>();

        Console.WriteLine($"Scoped, different scopes: same instance? {ReferenceEquals(first, second)}");
    }
}
