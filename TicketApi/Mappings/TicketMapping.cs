using TicketApi.Models;
using TicketApi.Dtos;

namespace TicketApi.Mappings;

public static class TicketMapping
{
    public static TicketDto ToDto(this Ticket ticket)
    {
        return new (
            Id: ticket.Id,
            Title: ticket.Title,
            Description: ticket.Description,
            Priority: ticket.Priority,
            Status: ticket.Status,
            EstimatedHours: ticket.EstimatedHours,
            CreatedAt: ticket.CreatedAt,
            ResolvedAt: ticket.ResolvedAt,
            UrgencyScore: ((int)ticket.Priority + 1) * ticket.EstimatedHours
        );
    }

    public static Ticket ToEntity(this CreateTicketDto dto)
    {
        return new()
        {
            Title = dto.Title,
            Description = dto.Description,
            EstimatedHours = dto.EstimatedHours,
            Priority = dto.Priority,
            Status = TicketStatus.Open,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static void ApplyTo(this UpdateTicketDto dto, Ticket ticket)
    {
        ticket.Title = dto.Title;
        ticket.Description = dto.Description;
        ticket.EstimatedHours = dto.EstimatedHours;
    }
}
