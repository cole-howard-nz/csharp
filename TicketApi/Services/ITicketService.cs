using TicketApi.Dtos;
using TicketApi.Models;

namespace TicketApi.Services;

public enum ResolveResult
{
    Ok,
    NotFound,
    AlreadyResolved
}

public interface ITicketService
{
    Task<List<Ticket>> GetAllAsync(TicketPriority? priority, TicketStatus? status, CancellationToken ct); 
    Task<Ticket?> GetByIdAsync(int id, CancellationToken ct);
    Task<Ticket> CreateAsync(CreateTicketDto dto, CancellationToken ct);
    Task<bool> UpdateAsync(int id, UpdateTicketDto dto, CancellationToken ct);
    Task<bool> DeleteAsync(int id, CancellationToken ct);
    Task<(ResolveResult resolveResult, Ticket? ticket)> ResolveAsync(int id, CancellationToken ct);
};
