namespace WebApi.Models;

public class Order
{
    public int Id { get; set; }
    public string Item { get; set; } = "";
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? InternalNote { get; set; }
}
