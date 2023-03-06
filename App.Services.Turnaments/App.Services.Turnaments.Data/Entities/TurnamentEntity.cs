using App.Data;
using App.Data.Attributes;

namespace App.Services.Turnaments.Data.Entities
{
    [SearchIndexDefinition("search")]
    [CollectionDefinition(nameof(TurnamentEntity))]
    public class TurnamentEntity : BaseEntity
    {
        [IndexedProperty("search")]
        public string Name { get; set; }
        public string GameId { get; set; }
        public string[] MatchesId { get; set; }
        public string EventId { get; set; }
    }
}