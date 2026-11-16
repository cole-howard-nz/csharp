using Microsoft.AspNetCore.Mvc;
using WebApi.Mapping;
using WebApi.Models;
using WebApi.Data;
using WebApi.DTO;

namespace WebApi.Controllers;


[ApiController]
[Route("api/[controller]")]
public class OrdersController(OrderStore orderStore) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<OrderDTO>> GetAll([FromQuery] string? item) 
    {
        IEnumerable<Order> orders = orderStore.All();

        if (!string.IsNullOrWhiteSpace(item))
        {
            orders = orders.Where(o => string.Equals(o.Item, item, StringComparison.OrdinalIgnoreCase));
        }

        return Ok(orders.Select(o => o.ToDTO()));
    }

    [HttpGet("{id:int}")]
    public ActionResult<OrderDTO> GetSingle([FromRoute] int id)
    {
        Order? order = orderStore.Find(id);

        if (order == null)
        {
            return NotFound("Order not found.");
        }

        return Ok(order.ToDTO());
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(UpdateOrderDTO dto, [FromRoute] int id)
    {
        Order? order = orderStore.Find(id);

        if (order == null)
        {
            return NotFound();
        }

        dto.ApplyTo(order);
        return NoContent();
    }

    [HttpPost]
    public ActionResult<OrderDTO> Create(CreateOrderDTO dto) 
    {
        Order order = orderStore.Add(dto.ToEntity());
        OrderDTO orderDTO = order.ToDTO();
        return CreatedAtAction(nameof(GetSingle), new { orderDTO.Id }, orderDTO);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete([FromRoute] int id)
    {
        bool success = orderStore.Remove(id);

        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }
}
