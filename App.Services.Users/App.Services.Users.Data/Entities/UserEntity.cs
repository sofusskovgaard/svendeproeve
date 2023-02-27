using App.Data;
using App.Data.Attributes;

namespace App.Services.Users.Data.Entities;

[IndexDefinition("email", isUnique: true)]
[IndexDefinition("username", isUnique: true)]
[IndexDefinition("teams")]
[IndexDefinition("organizations")]
[CollectionDefinition(nameof(UserEntity))]
public class UserEntity : BaseEntity
{
    public string Firstname { get; set; }

    public string Lastname { get; set; }

    [IndexedProperty("username")]
    public string Username { get; set; }
    
    [IndexedProperty("email")]
    public string Email { get; set; }

    public string PasswordHash { get; set; }

    public string PasswordSalt { get; set; }

    public DateTime? DateOfBirth { get; set; }
    
    public string? ProfilePicture { get; set; }
    
    public string? CoverPicture { get; set; }

    public string? Bio { get; set; }
    
    //public object[] Games { get; set; }
    
    public bool IsInTeam { get; set; }
    
    //public object[] Intergrations { get; set; }
    
    //public byte[] Permissions { get; set; }

    [IndexedProperty("organizations")]
    public string[]? Organizations { get; set; }

    [IndexedProperty("teams")]
    public string[]? Teams { get; set; }
}