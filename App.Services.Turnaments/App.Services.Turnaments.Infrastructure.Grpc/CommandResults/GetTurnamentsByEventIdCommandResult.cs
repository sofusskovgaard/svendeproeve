using App.Common.Grpc;
using App.Services.Turnaments.Common.Dtos;
using ProtoBuf;

namespace App.Services.Turnaments.Infrastructure.Grpc.CommandResults
{
    [ProtoContract]
    public class GetTurnamentsByEventIdCommandResult : IGrpcCommandResult
    {
        [ProtoMember(1)]
        public GrpcCommandResultMetadata Metadata { get; set; }
        [ProtoMember(2)]
        public IEnumerable<TurnamentDto> TurnamentDtos { get; set; }
    }
}
