using App.Data;
using App.Data.Attributes;

namespace App.Services.Billing.Data.Entities;

[CollectionDefinition(nameof(BillingEntity))]
public class BillingEntity : BaseEntity
{
    public string OrderId { get; set; }

    public string TransactionId { get; set; }
}