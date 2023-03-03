using App.Infrastructure.Grpc;
using App.Services.Turnaments.Common.Dtos;
using ProtoBuf;

namespace App.Services.Turnaments.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class UpdateMatchGrpcCommandMessage : IGrpcCommandMessage
    {
        [ProtoMember(1)]
        public MatchDto MatchDto { get; set; }
    }
}
