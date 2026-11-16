using WebApi.DTO;
using WebApi.Models;

namespace WebApi.Mapping;

public static class OrderMapping
{
    public static OrderDTO ToDTO(this Order order)
    {
        return new 
        (
            Id: order.Id, 
            Item: order.Item, 
            Quantity: order.Quantity, 
            UnitPrice: order.UnitPrice, 
            Total: order.Quantity * order.UnitPrice
        );
    }

    public static Order ToEntity(this CreateOrderDTO dto)
    {
        return new()
        {
            Item = dto.Item,
            Quantity = dto.Quantity,
            UnitPrice = dto.UnitPrice,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static void ApplyTo(this UpdateOrderDTO dto, Order order)
    {
        order.Item = dto.Item;
        order.Quantity = dto.Quantity;
        order.UnitPrice = dto.UnitPrice;
    }
}
