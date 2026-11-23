using TicketApi.Data;
using TicketApi.Dtos;
using TicketApi.Models;
using TicketApi.Mappings;
using Microsoft.EntityFrameworkCore;

namespace TicketApi.Services;

public class TicketService(TicketDbContext db) : ITicketService
{
  public async Task<List<Ticket>> GetAllAsync(
    TicketPriority? priority, TicketStatus? status, CancellationToken ct)
  {
    IQueryable<Ticket> query = db.Tickets.AsNoTracking();

    if (priority is not null)
    {
        query = query.Where(t => t.Priority == priority);
    }
    
    if (status is not null)
    {
        query = query.Where(t => t.Status == status);
    }

    List<Ticket> tickets = await query.OrderBy(t => t.Id).ToListAsync(ct);
    return tickets;
  }

  public async Task<Ticket?> GetByIdAsync(
    int id, CancellationToken ct)
  {
    Ticket? ticket = await db.Tickets.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id, ct);
    return ticket;
  }

  public async Task<Ticket> CreateAsync(
    CreateTicketDto dto, CancellationToken ct)
  {
    Ticket ticket = dto.ToEntity();
    db.Tickets.Add(ticket);
    await db.SaveChangesAsync(ct);

    return ticket;
  }

  public async Task<bool> UpdateAsync(
    int id, UpdateTicketDto dto, CancellationToken ct)
  {
    Ticket? ticket = await db.Tickets.FirstOrDefaultAsync(t => t.Id == id, ct);

    if (ticket is null)
    {
        return false;
    }

    dto.ApplyTo(ticket);
    await db.SaveChangesAsync(ct);

    return true;
  }

  public async Task<bool> DeleteAsync(
    int id, CancellationToken ct)
  {
    Ticket? ticket = await db.Tickets.FirstOrDefaultAsync(t => t.Id == id, ct);

    if (ticket is null)
    {
        return false;
    }

    db.Tickets.Remove(ticket);
    await db.SaveChangesAsync(ct);

    return true;
  }

  public async Task<(ResolveResult resolveResult, Ticket? ticket)> ResolveAsync(
    int id, CancellationToken ct)
  {
    Ticket? ticket = await db.Tickets.FirstOrDefaultAsync(t => t.Id == id, ct);

    if (ticket is null)
    {
       return (ResolveResult.NotFound, null);
    }

    if (ticket.Status is TicketStatus.Resolved or TicketStatus.Closed)
    {
        return (ResolveResult.AlreadyFound, ticket);
    }

    ticket.Status = TicketStatus.Resolved;
    ticket.ResolvedAt = DateTime.UtcNow;
    await db.SaveChangesAsync(ct);

    return (ResolveResult.Ok, ticket);
  }
}