using System.ComponentModel.DataAnnotations;
using TicketApi.Models;

namespace TicketApi.Dtos;

public record TicketDto
(
    int Id,
    string Title,
    string Description,
    TicketPriority Priority,
    TicketStatus Status,
    int EstimatedHours,
    DateTime CreatedAt,
    DateTime? ResolvedAt,
    int UrgencyScore
);

public record CreateTicketDto
(
    [Required, StringLength(maximumLength: 100, MinimumLength = 5)]
    string Title,

    [Required, StringLength(maximumLength: 1000)]
    string Description,

    [EnumDataType(enumType: typeof(TicketPriority))]
    TicketPriority Priority,

    [Range(minimum: 1, maximum: 40)]
    int EstimatedHours
);

public record UpdateTicketDto
(
    [Required, Length(minimumLength: 5, maximumLength: 100)]
    string Title,
    
    [Required, MaxLength(length: 1000)]
    string Description,

    [Range(minimum: 1, maximum: 40)]
    int EstimatedHours
);
