using App.Infrastructure.Events;

namespace App.Services.Orders.Infrastructure.Events;

public class TicketOrderPaidEventMessage : IEventMessage
{
    public string OrderId { get; set; }

    public string TicketId { get; set; }
}