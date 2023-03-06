using App.Data;
using App.Data.Attributes;

namespace App.Services.Turnaments.Data.Entities
{
    [SearchIndexDefinition("search")]
    [CollectionDefinition(nameof(MatchEntity))]
    public class MatchEntity : BaseEntity
    {
        [IndexedProperty("search")]
        public string Name { get; set; }
        public string[] TeamsId { get; set; }
        public string TurnamentId { get; set; }
        public string WinningTeamId { get; set; }
    }
}
