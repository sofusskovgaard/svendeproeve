using ProtoBuf;

namespace App.Services.Tournaments.Common.Dtos;

[ProtoContract]
public class MatchDto
{
    [ProtoMember(1)]
    public string Id { get; set; }

    [ProtoMember(2)]
    public string Name { get; set; }

    [ProtoMember(3)]
    public string[] TeamsId { get; set; }

    [ProtoMember(4)]
    public string TurnamentId { get; set; }

    [ProtoMember(5)]
    public string WinningTeamId { get; set; }
}