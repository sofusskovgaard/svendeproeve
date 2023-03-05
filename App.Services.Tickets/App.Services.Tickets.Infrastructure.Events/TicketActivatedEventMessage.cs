using App.Infrastructure.Events;

namespace App.Services.Tickets.Infrastructure.Events
{
    public class TicketActivatedEventMessage : IEventMessage
    {
        public string TicketId { get; set; }
    }
}