using App.Data;
using App.Data.Attributes;
using ProtoBuf;

namespace App.Services.Teams.Data.Entities
{
    [ProtoContract]
    [CollectionDefinition(nameof(TeamEntity))]
    public class TeamEntity : BaseEntity
    {
        [ProtoMember(1)]
        public string Name { get; set; }
        [ProtoMember(2)]
        public string Bio { get; set; }
        [ProtoMember(3)]
        public string ProfilePicturePath { get; set; }
        [ProtoMember(4)]
        public string CoverPicturePath { get; set; }
        [ProtoMember(5)]
        public string GameId { get; set; }
        [ProtoMember(6)]
        public string[] MembersId { get; set; }
        [ProtoMember(7)]
        public string ManagerId { get; set; }
        [ProtoMember(8)]
        public string OrganizationId { get; set; }
    }
}