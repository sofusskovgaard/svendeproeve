using App.Infrastructure.Grpc;
using App.Services.Games.Common.Dtos;
using ProtoBuf;

namespace App.Services.Games.Infrastructure.Grpc.CommandResults
{
    [ProtoContract]
    public class GetGamesByNameCommandResult : IGrpcCommandResult
    {
        [ProtoMember(1)]
        public GrpcCommandResultMetadata Metadata { get; set; }

        [ProtoMember(2)]
        public IEnumerable<GameDto> GameDtos { get; set; }
    }
}
