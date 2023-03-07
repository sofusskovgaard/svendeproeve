using App.Data;
using App.Data.Attributes;

namespace App.Services.Authentication.Data.Entities;

[IndexDefinition("active")]
[CollectionDefinition(nameof(JwtKeyEntity))]
public class JwtKeyEntity : BaseEntity
{
    public string PrivateKey { get; set; }

    public string PublicKey { get; set; }

    [IndexedProperty("active")]
    public bool IsActive { get; set; } = true;
}