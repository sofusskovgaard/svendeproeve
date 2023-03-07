using ProtoBuf;

namespace App.Services.Authentication.Common.Dtos;

[ProtoContract]
public class RegisterDto
{
    [ProtoMember(1)]
    public string Id { get; set; }

    [ProtoMember(2)]
    public string Username { get; set; }

    [ProtoMember(3)]
    public string Email { get; set; }
}