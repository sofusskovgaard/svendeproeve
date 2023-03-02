using App.Infrastructure.Events;

namespace App.Services.Billing.Infrastructure.Events;

public class PendingOrderChargeCreatedEventMessage : IEventMessage
{
    public string OrderChargeId { get; set; }
}