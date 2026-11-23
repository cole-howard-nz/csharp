using System.Formats.Asn1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketApi.Data;
using TicketApi.Dtos;
using TicketApi.Mappings;
using TicketApi.Models;

namespace TicketApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketController(TicketDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TicketDto>>> GetAll(
        [FromQuery] TicketPriority? priority, [FromQuery] TicketStatus? status, CancellationToken ct)
    {
        IQueryable<Ticket> tickets = db.Tickets;

        bool validPriority = priority.HasValue && Enum.IsDefined(priority.Value);
        bool validStatus = status.HasValue && Enum.IsDefined(status.Value);

        if (validPriority)
        {
            tickets = tickets.Where(t => t.Priority == priority);
        }

        if (validStatus)
        {
            tickets = tickets.Where(t => t.Status == status);
        }

        if ((priority.HasValue && !validPriority) || (status.HasValue && !validStatus))
        {
            return BadRequest(error: "Invalid priority or status passed.");
        }

        IEnumerable<TicketDto> result = await tickets.Select(t => t.ToDto()).ToListAsync(ct);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TicketDto>> GetById(
        [FromRoute] int id, CancellationToken ct)
    {
        Ticket? ticket = await db.Tickets.FirstOrDefaultAsync(t => t.Id == id, ct);

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
        Ticket ticket = dto.ToEntity();
        db.Tickets.Add(ticket);
        await db.SaveChangesAsync(ct);

        TicketDto ticketDto = ticket.ToDto();

        return CreatedAtAction(nameof(GetById), new { ticketDto.Id }, ticketDto);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(
        [FromRoute] int id, [FromBody] UpdateTicketDto dto, CancellationToken ct)
    {
        Ticket? ticket = await db.Tickets.FirstOrDefaultAsync(t => t.Id == id, ct);

        if (ticket is null)
        {
            return NotFound("Ticket not found.");
        }

        dto.ApplyTo(ticket);
        await db.SaveChangesAsync(ct);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(
        [FromRoute] int id, CancellationToken ct)
    {
        Ticket? ticket = await db.Tickets.FirstOrDefaultAsync(t => t.Id == id, ct);

        if (ticket is null)
        {
            return NotFound("Ticket not found.");
        }

        db.Tickets.Remove(ticket);
        await db.SaveChangesAsync(ct);

        return NoContent();
    }

    [HttpPost("{id:int}/resolve")]
    public async Task<ActionResult<TicketDto>> Resolve(
        [FromRoute] int id, CancellationToken ct)
    {
        Ticket? ticket = await db.Tickets.FirstOrDefaultAsync(t => t.Id == id, ct);

        if (ticket is null)
        {
            return NotFound("Ticket not found.");
        }

        if (ticket.Status is TicketStatus.Resolved || ticket.Status is TicketStatus.Closed)
        {
            return Conflict("Ticket is already resolved or is closed.");
        }

        ticket.Status = TicketStatus.Resolved;
        ticket.ResolvedAt = DateTime.UtcNow;
        await db.SaveChangesAsync(ct);

        TicketDto ticketDto = ticket.ToDto();
        return Ok(ticketDto);
    }
}
