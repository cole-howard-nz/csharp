using WebApi.Models;

namespace WebApi.Data;

public class OrderStore
{
    private readonly List<Order> _orders =
    [
        new() { Id = 1, Item = "Peanut Butter", Quantity = 2, UnitPrice = 5.99m, CreatedAt = new DateTime(2026, 11, 1), InternalNote = "supplier margin 22%" },
        new() { Id = 2, Item = "Jam", Quantity = 1, UnitPrice = 4.50m, CreatedAt = new DateTime(2026, 11, 3), InternalNote = "VIP customer" },
        new() { Id = 3, Item = "Bread", Quantity = 3, UnitPrice = 3.20m, CreatedAt = new DateTime(2026, 11, 5) }
    ];
    private int _nextId = 4;
    private readonly Lock _lock = new();

    public List<Order> All()
    {
        lock (_lock) return [.. _orders];
    }

    public Order? Find(int id)
    {
        lock (_lock) return _orders.FirstOrDefault(o => o.Id == id);
    }

    public Order Add(Order order)
    {
        lock (_lock)
        {
            order.Id = _nextId++;
            _orders.Add(order);
            return order;
        }
    }

    public bool Remove(int id)
    {
        lock (_lock)
        {
            Order? order = Find(id);
            return order is not null && _orders.Remove(order);
        }
    }
}
