using App.Data;
using App.Data.Attributes;
using MongoDB.Bson.Serialization.Attributes;

namespace App.Services.Users.Data.Entities;

[SearchIndexDefinition("search")]
[IndexDefinition("email_username")]
[IndexDefinition("teams")]
[IndexDefinition("organizations")]
[IndexDefinition("games")]
[CollectionDefinition(nameof(UserEntity))]
public class UserEntity : BaseEntity
{
    [IndexedProperty("search")]
    public string Firstname { get; set; }

    [IndexedProperty("search")]
    public string Lastname { get; set; }

    [IndexedProperty("search")]
    [IndexedProperty("email_username")]
    public string Username { get; set; }

    [IndexedProperty("search")]
    [IndexedProperty("email_username")]
    public string Email { get; set; }

    public DateTime? DateOfBirth { get; set; }
    
    public string? ProfilePicture { get; set; }
    
    public string? CoverPicture { get; set; }

    [IndexedProperty("search")]
    public string? Bio { get; set; }

    [BsonIgnore]
    public bool IsInTeam => Teams?.Length > 0;

    [IndexedProperty("organizations")]
    public string[]? Organizations { get; set; }

    [IndexedProperty("teams")]
    public string?[] Teams { get; set; }

    [IndexedProperty("games")]
    public string[]? Games { get; set; }
}