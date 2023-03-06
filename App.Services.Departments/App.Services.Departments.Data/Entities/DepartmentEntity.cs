using App.Data;
using App.Data.Attributes;

namespace App.Services.Departments.Data.Entities;

[SearchIndexDefinition("search")]
[CollectionDefinition(nameof(DepartmentEntity))]
public class DepartmentEntity : BaseEntity
{
    [IndexedProperty("search", 2)]
    public string Name { get; set; }

    [IndexedProperty("search")]
    public string Address { get; set; }

    public string[] OrganizationIds { get; set; }
}