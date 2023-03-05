using App.Infrastructure.Events;

namespace App.Services.Billing.Infrastructure.Events;

public class OrderChargePaidEventMessage : IEventMessage
{
    public string OrderId { get; set; }

    public string OrderChargeId { get; set; }

    public string TransactionNumber { get; set; }
}