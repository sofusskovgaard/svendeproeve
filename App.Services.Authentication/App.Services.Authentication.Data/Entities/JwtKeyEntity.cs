using App.Data;
using App.Data.Attributes;

namespace App.Services.Authentication.Data.Entities;

[CollectionDefinition(nameof(JwtKeyEntity))]
public class JwtKeyEntity : BaseEntity
{
    public string PrivateKey { get; set; }

    public string PublicKey { get; set; }

    public bool IsActive { get; set; } = true;
}