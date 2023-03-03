using App.Infrastructure.Grpc;
using App.Services.Games.Common.Dtos;
using ProtoBuf;

namespace App.Services.Games.Infrastructure.Grpc.CommandResults
{
    [ProtoContract]
    public class GetGameByIdGrpcCommandResult : IGrpcCommandResult
    {
        [ProtoMember(1)]
        public GrpcCommandResultMetadata Metadata { get; set; }

        [ProtoMember(2)]
        public GameDto GameDto { get; set; }
    }
}
