using App.Data;
using App.Data.Attributes;

namespace App.Services.Billing.Data.Entities;

[CollectionDefinition(nameof(OrderChargeEntity))]
public class OrderChargeEntity : BaseEntity
{
    public string OrderId { get; set; }

    public string Status { get; set; }

    public decimal Amount { get; set; }

    public string? TransactionNumber { get; set; }
}