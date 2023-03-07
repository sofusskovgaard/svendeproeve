using App.Services.Tickets.Common.Records;

namespace App.Services.Gateway.Common;

public record BookTicketsModel(TicketBooking[] Bookings);