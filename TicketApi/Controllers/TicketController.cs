using Microsoft.AspNetCore.Mvc;
using TicketApi.Dtos;
using TicketApi.Mappings;
using TicketApi.Models;
using TicketApi.Services;

namespace TicketApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketController(ITicketService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TicketDto>>> GetAll(
        [FromQuery] TicketPriority? priority, [FromQuery] TicketStatus? status, CancellationToken ct)
    {
        if ((priority.HasValue && !Enum.IsDefined(priority.Value)) || (status.HasValue && !Enum.IsDefined(status.Value)))
        {
            return BadRequest(error: "Invalid priority or status passed.");
        }

        List<Ticket> tickets = await service.GetAllAsync(priority, status, ct);
        List<TicketDto> ticketDtos = [.. tickets.Select(t => t.ToDto())];
        return Ok(ticketDtos);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TicketDto>> GetById(
        [FromRoute] int id, CancellationToken ct)
    {
        Ticket? ticket = await service.GetByIdAsync(id, ct);

        if (ticket is null)
        {
            return NotFound("Ticket not found.");
        }

        TicketDto ticketDto = ticket.ToDto();
        return Ok(ticketDto);
    }

    [HttpPost]
    public async Task<ActionResult<TicketDto>> Create(
        [FromBody] CreateTicketDto dto, CancellationToken ct)
    {
        Ticket ticket = await service.CreateAsync(dto, ct);
        TicketDto ticketDto = ticket.ToDto();

        return CreatedAtAction(nameof(GetById), new { ticketDto.Id }, ticketDto);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(
        [FromRoute] int id, [FromBody] UpdateTicketDto dto, CancellationToken ct)
    {
        bool success = await service.UpdateAsync(id, dto, ct);

        if (!success)
        {
            return NotFound("Ticket not found.");
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(
        [FromRoute] int id, CancellationToken ct)
    {
        bool success = await service.DeleteAsync(id, ct);

        if (!success)
        {
            return NotFound("Ticket not found.");
        }

        return NoContent();
    }

    [HttpPost("{id:int}/resolve")]
    public async Task<ActionResult<TicketDto>> Resolve(
        [FromRoute] int id, CancellationToken ct)
    {
        (ResolveResult resolveResult, Ticket? ticket) = await service.ResolveAsync(id, ct);

        if (ticket is null || resolveResult is ResolveResult.NotFound)
        {
            return NotFound("Ticket not found.");
        }

        if (resolveResult is ResolveResult.AlreadyFound)
        {
            return Conflict("Ticket is already resolved or is closed.");
        }

        return Ok(ticket.ToDto());
    }
}
