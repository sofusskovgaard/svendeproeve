using ProtoBuf;

namespace App.Services.Turnaments.Common.Dtos
{
    [ProtoContract]
    public class TurnamentDto
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
}
