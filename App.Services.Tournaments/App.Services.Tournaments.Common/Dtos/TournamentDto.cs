using ProtoBuf;

namespace App.Services.Tournaments.Common.Dtos;

[ProtoContract]
public class TournamentDto
{
    [ProtoMember(1)]
    public string Id { get; set; }

    [ProtoMember(2)]
    public string Name { get; set; }

    [ProtoMember(3)]
    public string GameId { get; set; }

    [ProtoMember(4)]
    public string[] MatchesId { get; set; }

    [ProtoMember(5)]
    public string EventId { get; set; }
}