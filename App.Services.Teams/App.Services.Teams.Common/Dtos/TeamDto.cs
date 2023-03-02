using ProtoBuf;

namespace App.Services.Teams.Common.Dtos
{
    [ProtoContract]
    public class TeamDto
    {
        [ProtoMember(1)]
        public string Id { get; set; }
        [ProtoMember(2)]
        public string Name { get; set; }
        [ProtoMember(3)]
        public string Bio { get; set; }
        [ProtoMember(4)]
        public string ProfilePicturePath { get; set; }
        [ProtoMember(5)]
        public string CoverPicturePath { get; set; }
        [ProtoMember(6)]
        public string GameId { get; set; }
        [ProtoMember(7)]
        public string[] MembersId { get; set; }
        [ProtoMember(8)]
        public string ManagerId { get; set; }
        [ProtoMember(9)]
        public string OrganizationId { get; set; }
    }
}
