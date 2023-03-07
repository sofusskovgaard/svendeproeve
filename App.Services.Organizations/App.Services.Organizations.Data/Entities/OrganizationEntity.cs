using App.Data;
using App.Data.Attributes;

namespace App.Services.Organizations.Data.Entities;

[IndexDefinition("members")]
[IndexDefinition("teams")]
[IndexDefinition("department")]
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

    [IndexedProperty("members")]
    public string[]? MemberIds { get; set; }

    [IndexedProperty("teams")]
    public string[]? TeamIds { get; set; }

    [IndexedProperty("search")]
    public string? Address { get; set; }

    [IndexedProperty("department")]
    public string? DepartmentId { get; set; }
}