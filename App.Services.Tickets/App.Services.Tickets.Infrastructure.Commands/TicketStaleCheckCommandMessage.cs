using App.Infrastructure.Commands;
using App.Infrastructure.Events;

namespace App.Services.Tickets.Infrastructure.Commands;

public class TicketStaleCheckCommandMessage : ICommandMessage
{
    public string?[] Tickets { get; set; }
}