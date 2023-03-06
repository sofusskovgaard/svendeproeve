using App.Data;
using App.Data.Attributes;

namespace App.Services.Teams.Data.Entities
{
    [SearchIndexDefinition("search")]
    [IndexDefinition("name", isUnique: true)]
    [IndexDefinition("game")]
    [IndexDefinition("members")]
    [IndexDefinition("manager")]
    [IndexDefinition("organization")]
    [CollectionDefinition(nameof(TeamEntity))]
    public class TeamEntity : BaseEntity
    {
        [IndexedProperty("search")]
        [IndexedProperty("name")]
        public string Name { get; set; }
        [IndexedProperty("search")]
        public string Bio { get; set; }
        public string ProfilePicturePath { get; set; }
        public string CoverPicturePath { get; set; }
        [IndexedProperty("game")]
        public string GameId { get; set; }
        [IndexedProperty("members")]
        public string[] MembersId { get; set; }
        [IndexedProperty("manager")]
        public string ManagerId { get; set; }
        [IndexedProperty("organization")]
        public string OrganizationId { get; set; }
    }
}