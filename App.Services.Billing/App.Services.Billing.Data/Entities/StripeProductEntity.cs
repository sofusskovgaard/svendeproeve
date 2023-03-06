using App.Data;
using App.Data.Attributes;

namespace App.Services.Billing.Data.Entities;

[CollectionDefinition(nameof(StripeProductEntity))]
public class StripeProductEntity : BaseEntity
{
    public string Type { get; set; }

    public string[] Prices { get; set; }
}