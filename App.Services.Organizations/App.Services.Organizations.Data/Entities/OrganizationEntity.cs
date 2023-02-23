using App.Data;
using App.Data.Attributes;
using ProtoBuf;

namespace App.Services.Organizations.Data.Entities
{
    [ProtoContract]
    [CollectionDefinition(nameof(OrganizationEntity))]
    public class OrganizationEntity : BaseEntity
    {
        [ProtoMember(1)]
        public string Name { get; set; }
        [ProtoMember(2)]
        public string? Bio { get; set; }
        [ProtoMember(3)]
        public string? ProfilePicture { get; set; }
        [ProtoMember(4)]
        public string? CoverPicture { get; set; }
        [ProtoMember(5)]
        public string[]? MemberIds { get; set; }
        [ProtoMember(6)]
        public string[]? GameIds { get; set; }
        [ProtoMember(7)]
        public string[]? TeamIds { get; set; }
        [ProtoMember(8)]
        public string? Address { get; set; }
    }
}