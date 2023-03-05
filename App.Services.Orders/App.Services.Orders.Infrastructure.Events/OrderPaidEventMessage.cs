using App.Infrastructure.Events;

namespace App.Services.Orders.Infrastructure.Events;

public class OrderPaidEventMessage : IEventMessage
{
    public string OrderId { get; set; }

    public string? ReferenceType { get; set; }

    public string? ReferenceId { get; set; }
}