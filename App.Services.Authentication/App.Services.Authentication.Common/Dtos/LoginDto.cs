using ProtoBuf;

namespace App.Services.Authentication.Common.Dtos;

[ProtoContract]
public class LoginDto
{
    [ProtoMember(1)]
    public string AccessToken { get; set; }

    [ProtoMember(2)]
    public string RefreshToken { get; set; }

    [ProtoMember(3)]
    public int ExpiresIn { get; set; }

    [ProtoMember(4)]
    public string Type { get; set; }
}