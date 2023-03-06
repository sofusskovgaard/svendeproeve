using App.Infrastructure.Commands;
using App.Services.Tickets.Common.Records;

namespace App.Services.Tickets.Infrastructure.Commands;

public class BookTicketsCommandMessage : ICommandMessage
{
    public string UserId { get; set; }

    public TicketBooking[] Bookings { get; set; }
}