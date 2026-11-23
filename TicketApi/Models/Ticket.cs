namespace TicketApi.Models;

public class Ticket
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public TicketPriority Priority { get; set; }
    public TicketStatus Status { get; set; }
    public int EstimatedHours { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ResolvedAt { get; set; }
    public string? InternalNote { get; set; }
}
