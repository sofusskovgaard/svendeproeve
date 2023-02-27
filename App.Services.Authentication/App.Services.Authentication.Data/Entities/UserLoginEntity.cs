using App.Data;
using App.Data.Attributes;

namespace App.Services.Authentication.Data.Entities
{
    [IndexDefinition("username", isUnique: true)]
    [IndexDefinition("email", isUnique: true)]
    [CollectionDefinition(nameof(UserLoginEntity))]
    public class UserLoginEntity : BaseEntity
    {
        [IndexedProperty("username")]
        public string Username { get; set; }

        [IndexedProperty("email")]
        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string PasswordSalt { get; set; }
    }
}