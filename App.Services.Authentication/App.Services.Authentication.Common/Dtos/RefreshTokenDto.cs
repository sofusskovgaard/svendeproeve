using ProtoBuf;

namespace App.Services.Authentication.Common.Dtos;

[ProtoContract]
public class RefreshTokenDto
{
    [ProtoMember(1)]
    public string AccessToken { get; set; }

    [ProtoMember(2)]
    public int ExpiresIn { get; set; }

    [ProtoMember(3)]
    public string Type { get; set; } = "Bearer";
}