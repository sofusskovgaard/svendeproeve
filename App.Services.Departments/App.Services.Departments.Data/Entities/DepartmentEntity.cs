using App.Data;
using App.Data.Attributes;

namespace App.Services.Departments.Data.Entities;

[IndexDefinition("organizations")]
[SearchIndexDefinition("search")]
[CollectionDefinition(nameof(DepartmentEntity))]
public class DepartmentEntity : BaseEntity
{
    [IndexedProperty("search", 2)]
    public string Name { get; set; }

    [IndexedProperty("search")]
    public string Address { get; set; }

    [IndexedProperty("organizations")]
    public string[] OrganizationIds { get; set; }
}