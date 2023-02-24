using App.Data;
using App.Data.Attributes;

namespace App.Services.Teams.Data.Entities
{
    [IndexDefinition("name", isUnique: true)]
    [CollectionDefinition(nameof(TeamEntity))]
    public class TeamEntity : BaseEntity
    {
        [IndexedProperty("name")]
        public string Name { get; set; }
        public string Bio { get; set; }
        public string ProfilePicturePath { get; set; }
        public string CoverPicturePath { get; set; }
        public string GameId { get; set; }
        public string[] MembersId { get; set; }
        public string ManagerId { get; set; }
        public string OrganizationId { get; set; }
    }
}