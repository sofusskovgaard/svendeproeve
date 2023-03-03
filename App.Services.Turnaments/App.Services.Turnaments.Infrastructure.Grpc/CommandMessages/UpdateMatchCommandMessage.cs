using App.Common.Grpc;
using App.Services.Turnaments.Common.Dtos;
using ProtoBuf;

namespace App.Services.Turnaments.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class UpdateMatchCommandMessage : IGrpcCommandMessage
    {
        [ProtoMember(1)]
        public MatchDto MatchDto { get; set; }
    }
}
