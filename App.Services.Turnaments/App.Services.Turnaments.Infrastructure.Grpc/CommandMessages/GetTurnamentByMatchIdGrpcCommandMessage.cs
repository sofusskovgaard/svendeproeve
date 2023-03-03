using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Turnaments.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class GetTurnamentByMatchIdGrpcCommandMessage : GrpcCommandMessage
    {
        [ProtoMember(1)]
        public string MatchId { get; set; }

        [ProtoMember(100)]
        public override GrpcCommandMessageMetadata? Metadata { get; set; }
    }
}
