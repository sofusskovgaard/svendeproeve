using App.Data;
using App.Data.Attributes;

namespace App.Services.Tournaments.Data.Entities;

[IndexDefinition("game")]
[IndexDefinition("matches")]
[IndexDefinition("shard")]
[SearchIndexDefinition("search")]
[CollectionDefinition(nameof(TournamentEntity))]
public class TournamentEntity : BaseEntity
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