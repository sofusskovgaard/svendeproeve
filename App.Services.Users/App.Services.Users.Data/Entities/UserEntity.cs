using App.Data;
using App.Data.Attributes;
using MongoDB.Bson.Serialization.Attributes;

namespace App.Services.Users.Data.Entities;

[IndexDefinition("email_username")]
[IndexDefinition("teams")]
[IndexDefinition("organizations")]
[IndexDefinition("games")]
[CollectionDefinition(nameof(UserEntity))]
public class UserEntity : BaseEntity
{
    public string Firstname { get; set; }

    public string Lastname { get; set; }

    [IndexedProperty("email_username")]
    public string Username { get; set; }
    
    [IndexedProperty("email_username")]
    public string Email { get; set; }

    public DateTime? DateOfBirth { get; set; }
    
    public string? ProfilePicture { get; set; }
    
    public string? CoverPicture { get; set; }

    public string? Bio { get; set; }

    public bool IsInTeam { get; set; }

    [IndexedProperty("organizations")]
    public string[]? Organizations { get; set; }

    [IndexedProperty("teams")]
    public string[]? Teams { get; set; }

    [IndexedProperty("games")]
    public string[]? Games { get; set; }
}