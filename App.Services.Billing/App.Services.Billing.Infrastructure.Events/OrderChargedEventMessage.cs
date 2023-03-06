using App.Infrastructure.Events;

namespace App.Services.Billing.Infrastructure.Events;

public class OrderChargedEventMessage : IEventMessage
{
    public string OrderChargeId { get; set; }
}