using App.Data;
using App.Data.Attributes;

namespace App.Services.Turnaments.Data.Entities
{
    [IndexDefinition("tournament")]
    [IndexDefinition("teams")]
    [IndexDefinition("winning_team")]
    [SearchIndexDefinition("search")]
    [CollectionDefinition(nameof(MatchEntity))]
    public class MatchEntity : BaseEntity
    {
        [IndexedProperty("search")]
        public string Name { get; set; }
        [IndexedProperty("teams")]
        public string[] TeamsId { get; set; }
        [IndexedProperty("tournament")]
        public string TurnamentId { get; set; }
        [IndexedProperty("winning_team")]
        public string WinningTeamId { get; set; }
    }
}
