using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Turnaments.Infrastructure.Grpc.CommandResults
{
    [ProtoContract]
    public class CreateMatchCommandResult : IGrpcCommandResult
    {
        [ProtoMember(1)]
        public GrpcCommandResultMetadata Metadata { get; set; }
        [ProtoMember(2)]
        public string Id { get; set; }
    }
}
