using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Organizations.Common.Dtos
{
    [ProtoContract]
    public class OrganizationDto
    {
        [ProtoMember(1)]
        public string Id { get; set; }
        [ProtoMember(2)]
        public string Name { get; set; }
        [ProtoMember(3)]
        public string Bio { get; set; }
        [ProtoMember(4)]
        public string ProfilePicture { get; set; }
        [ProtoMember(5)]
        public string CoverPicture { get; set; }
        [ProtoMember(6)]
        public string[] MemberIds { get; set; }
        [ProtoMember(7)]
        public string[] GameIds { get; set; }
        [ProtoMember(8)]
        public string[] TeamIds { get; set; }
        [ProtoMember(9)]
        public string? Address { get; set; }
        [ProtoMember(10)]
        public string? DepartmentId { get; set; }
    }
}
