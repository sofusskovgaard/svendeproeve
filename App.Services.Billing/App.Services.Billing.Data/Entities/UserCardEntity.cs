using App.Data;
using App.Data.Attributes;

namespace App.Services.Billing.Data.Entities;

[CollectionDefinition(nameof(UserCardEntity))]
public class UserCardEntity : BaseEntity
{
    public string UserId { get; set; }
}