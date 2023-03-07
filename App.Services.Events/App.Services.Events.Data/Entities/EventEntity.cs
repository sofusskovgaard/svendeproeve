using App.Data;
using App.Data.Attributes;

namespace App.Services.Events.Data.Entities;

[IndexDefinition("department")]
[IndexDefinition("tournaments")]
[SearchIndexDefinition("search")]
[CollectionDefinition(nameof(EventEntity))]
public class EventEntity : BaseEntity
{
    [IndexedProperty("search")]
    public string EventName { get; set; }

    [IndexedProperty("search")]
    public string Location { get; set; }

    [IndexedProperty("tournaments")]
    public string[] Tournaments { get; set; }

    [IndexedProperty("department")]
    public string DepartmentId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }
}