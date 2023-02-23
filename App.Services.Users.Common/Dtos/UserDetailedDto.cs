using ProtoBuf;

namespace App.Services.Users.Common.Dtos;

[ProtoContract]
public class UserDetailedDto : UserDto
{
    [ProtoMember(1)]
    public object[] Intergrations { get; set; }

    [ProtoMember(2)]
    public byte[] Permissions { get; set; }
}