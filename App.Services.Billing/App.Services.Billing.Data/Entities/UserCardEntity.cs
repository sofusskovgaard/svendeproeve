using App.Data;
using App.Data.Attributes;

namespace App.Services.Billing.Data.Entities;

[IndexDefinition("shard")]
[CollectionDefinition(nameof(UserCardEntity))]
public class UserCardEntity : BaseEntity
{
    [IndexedProperty("shard", true)]
    public string UserId { get; set; }

    public string Name { get; set; }

    public string Mask { get; set; }

    public string ExternalId { get; set; }

    public string CardType { get; set; }
}