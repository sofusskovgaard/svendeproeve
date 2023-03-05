using ProtoBuf;

namespace App.Services.Games.Common.Dtos;

[ProtoContract]
public class GameDto
{
    [ProtoMember(1)]
    public string Id { get; set; }

    [ProtoMember(2)]
    public string Name { get; set; }

    [ProtoMember(3)]
    public string Discription { get; set; }

    [ProtoMember(4)]
    public string ProfilePicture { get; set; }

    [ProtoMember(5)]
    public string CoverPicture { get; set; }

    [ProtoMember(6)]
    public string[] Genre { get; set; }
}