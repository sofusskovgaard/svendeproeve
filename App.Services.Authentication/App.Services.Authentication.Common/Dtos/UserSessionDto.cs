using ProtoBuf;

namespace App.Services.Authentication.Common.Dtos;

[ProtoContract]
public class UserSessionDto
{
    [ProtoMember(1)]
    public string Id { get; set; }

    [ProtoMember(2)]
    public string? UserId { get; set; } = null;

    [ProtoMember(3)]
    public string? IP { get; set; } = null;

    [ProtoMember(4)]
    public string? UserAgent { get; set; } = null;

    [ProtoMember(5)]
    public DateTime CreatedTs { get; set; }
}