using App.Data;
using App.Data.Attributes;
using ProtoBuf;

namespace App.Services.Organizations.Data.Entities
{
    [CollectionDefinition(nameof(OrganizationEntity))]
    public class OrganizationEntity : BaseEntity
    {
        public string Name { get; set; }
        public string? Bio { get; set; }
        public string? ProfilePicture { get; set; }
        public string? CoverPicture { get; set; }
        public string[]? MemberIds { get; set; }
        public string[]? GameIds { get; set; }
        public string[]? TeamIds { get; set; }
        public string? Address { get; set; }
    }
}