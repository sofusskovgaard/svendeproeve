using ProtoBuf;

namespace App.Services.Departments.Common.Dtos;

[ProtoContract]
public class DepartmentDto
{
    [ProtoMember(1)]
    public string Id { get; set; }

    [ProtoMember(2)]
    public string Name { get; set; }

    [ProtoMember(3)]
    public string Address { get; set; }

    [ProtoMember(4)]
    public string[] OrganizationIds { get; set; }

    [ProtoMember(5)]
    public DateTime CreateTs { get; set; }

    [ProtoMember(6)]
    public DateTime? UpdatedTs { get; set; }
}