using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Turnaments.Infrastructure.Grpc.CommandResults
{
    [ProtoContract]
    public class CreateTurnamentCommandResult : IGrpcCommandResult
    {
        [ProtoMember(1)]
        public GrpcCommandResultMetadata Metadata { get; set; }
    }
}
