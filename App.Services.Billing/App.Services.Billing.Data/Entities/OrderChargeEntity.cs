using App.Data;
using App.Data.Attributes;

namespace App.Services.Billing.Data.Entities;

[IndexDefinition("order")]
[IndexDefinition("status")]
[CollectionDefinition(nameof(OrderChargeEntity))]
public class OrderChargeEntity : BaseEntity
{
    [IndexedProperty("order")]
    public string OrderId { get; set; }

    [IndexedProperty("status")]
    public string Status { get; set; }

    public decimal Amount { get; set; }

    public string? TransactionNumber { get; set; }
}