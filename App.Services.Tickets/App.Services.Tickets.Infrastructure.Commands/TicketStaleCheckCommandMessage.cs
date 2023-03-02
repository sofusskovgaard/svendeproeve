using App.Infrastructure.Events;

namespace App.Services.Tickets.Infrastructure.Commands;

public class TicketStaleCheckCommandMessage : IEventMessage
{
    public string TicketId { get; set; }
}