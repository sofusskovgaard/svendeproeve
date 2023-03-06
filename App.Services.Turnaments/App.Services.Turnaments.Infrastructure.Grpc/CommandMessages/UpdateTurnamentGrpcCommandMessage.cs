using App.Common.Grpc;
using App.Services.Turnaments.Common.Dtos;
using ProtoBuf;

namespace App.Services.Turnaments.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class UpdateTurnamentGrpcCommandMessage : GrpcCommandMessage
    {
        [ProtoMember(1)]
        public TurnamentDto TurnamentDto { get; set; }

        [ProtoMember(100)]
        public override GrpcCommandMessageMetadata? Metadata { get; set; }
    }
}
