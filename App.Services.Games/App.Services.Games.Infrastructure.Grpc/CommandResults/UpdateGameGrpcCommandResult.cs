using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Games.Infrastructure.Grpc.CommandResults
{
    [ProtoContract]
    public class UpdateGameGrpcCommandResult : IGrpcCommandResult
    {
        [ProtoMember(1)]
        public GrpcCommandResultMetadata Metadata { get; set; }
    }
}
