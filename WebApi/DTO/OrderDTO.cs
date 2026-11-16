using System.ComponentModel.DataAnnotations;

namespace WebApi.DTO;

public record OrderDTO(
    int Id, 
    string Item, 
    int Quantity, 
    decimal UnitPrice, 
    decimal Total
);

public record CreateOrderDTO(
    [Required, StringLength(100, MinimumLength = 1)] string Item, 
    [Required, Range(1, 1000)] int Quantity, 
    [Required, Range(0.01, 100000)] decimal UnitPrice
);

public record UpdateOrderDTO(
    [Required, StringLength(100, MinimumLength = 1)] string Item, 
    [Required, Range(1, 1000)] int Quantity, 
    [Required, Range(0.01, 100000)] decimal UnitPrice
);
