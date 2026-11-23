using Microsoft.EntityFrameworkCore;
using TicketApi.Models;

namespace TicketApi.Data;

public class TicketDbContext(DbContextOptions<TicketDbContext> options) : DbContext(options)
{
    public DbSet<Ticket> Tickets => Set<Ticket>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.Property(t => t.Title).IsRequired().HasMaxLength(maxLength: 100);
            entity.Property(t => t.Description).IsRequired().HasMaxLength(maxLength: 1000);
            entity.Property(t => t.Priority).HasConversion<string>().HasMaxLength(maxLength: 20);
            entity.Property(t => t.Status).HasConversion<string>().HasMaxLength(maxLength: 20);
            entity.Property(t => t.InternalNote).HasMaxLength(maxLength: 200);
        });

        modelBuilder.Entity<Ticket>().HasData(
            new Ticket { Id = 1, Title = "Printer jams on level 3", Description = "Paper jams every few pages.", Priority = TicketPriority.Medium, Status = TicketStatus.Open, EstimateHours = 2, CreatedAt = new DateTime(2026, 11, 01, 0, 0, 0, DateTimeKind.Utc), ResovledAt = null, InternalNote = "Reported by facilities" },
            new Ticket { Id = 2, Title = "VPN drops every hour", Description = "Connection dies after about 60 minutes.", Priority = TicketPriority.High, Status = TicketStatus.InProgress, EstimateHours = 6, CreatedAt = new DateTime(2026, 11, 02, 0, 0, 0, DateTimeKind.Utc) },
            new Ticket { Id = 3, Title = "Cannot log in to payroll", Description = "Password reset email never arrives.", Priority = TicketPriority.Urgent, Status = TicketStatus.Open, EstimateHours = 3, CreatedAt = new DateTime(2026, 11, 03, 0, 0, 0, DateTimeKind.Utc), InternalNote = "Check mail relay first" },
            new Ticket { Id = 4, Title = "Update desk monitor drivers", Description = "Drivers are two versions behind.", Priority = TicketPriority.Low, Status = TicketStatus.Resolved, EstimateHours = 1, CreatedAt = new DateTime(2026, 10, 28, 0, 0, 0, DateTimeKind.Utc), ResovledAt = null }
        );
    }
}
