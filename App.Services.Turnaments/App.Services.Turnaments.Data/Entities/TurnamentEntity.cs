using App.Data;
using App.Data.Attributes;

namespace App.Services.Turnaments.Data.Entities;

[IndexDefinition("game")]
[IndexDefinition("matches")]
[IndexDefinition("shard")]
[SearchIndexDefinition("search")]
[CollectionDefinition(nameof(TurnamentEntity))]
public class TurnamentEntity : BaseEntity
{
    [IndexedProperty("search")]
    public string Name { get; set; }

    [IndexedProperty("game")]
    public string GameId { get; set; }

    [IndexedProperty("matches")]
    public string[] MatchesId { get; set; }

    [IndexedProperty("shard", true)]
    public string EventId { get; set; }
}