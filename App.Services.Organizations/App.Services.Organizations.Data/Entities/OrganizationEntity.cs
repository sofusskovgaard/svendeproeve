using App.Data;
using App.Data.Attributes;

namespace App.Services.Organizations.Data.Entities
{
    [SearchIndexDefinition("search")]
    [CollectionDefinition(nameof(OrganizationEntity))]
    public class OrganizationEntity : BaseEntity
    {
        [IndexedProperty("search")]
        public string Name { get; set; }
        [IndexedProperty("search")]
        public string? Bio { get; set; }
        public string? ProfilePicture { get; set; }
        public string? CoverPicture { get; set; }
        public string[]? MemberIds { get; set; }
        public string[]? TeamIds { get; set; }
        [IndexedProperty("search")]
        public string? Address { get; set; }
        public string? DepartmentId { get; set; }
    }
}