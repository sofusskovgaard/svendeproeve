using ProtoBuf;

namespace App.Services.Users.Common.Dtos;

[ProtoContract]
[ProtoInclude(12, typeof(UserDetailedDto))]
public class UserDto
{
    [ProtoMember(1)]
    public string Id { get; set; }

    [ProtoMember(2)]
    public string Firstname { get; set; }

    [ProtoMember(3)]
    public string Lastname { get; set; }

    [ProtoMember(4)]
    public string Username { get; set; }

    [ProtoMember(5)]
    public string? ProfilePicture { get; set; }

    [ProtoMember(6)]
    public string? CoverPicture { get; set; }

    [ProtoMember(7)]
    public string? Bio { get; set; }

    [ProtoMember(8)]
    public string[]? Games { get; set; }

    [ProtoMember(9)]
    public string[]? Teams { get; set; }

    [ProtoMember(10)]
    public string[]? Organizations { get; set; }
}