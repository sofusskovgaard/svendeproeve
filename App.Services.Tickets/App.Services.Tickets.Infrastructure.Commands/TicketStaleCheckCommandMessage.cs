using App.Infrastructure.Commands;

namespace App.Services.Tickets.Infrastructure.Commands;

public class TicketStaleCheckCommandMessage : ICommandMessage
{
    public string?[] Tickets { get; set; }
}