using App.Infrastructure.Grpc;
using App.Services.Orders.Common.Test;
using ProtoBuf;

namespace App.Services.Orders.Infrastructure.Grpc.CommandResults
{
    [ProtoContract]
    public class GetByIdGrpcCommandResult : IGrpcCommandResult
    {
        [ProtoMember(1)]
        public GrpcCommandResultMetadata Metadata { get; set; }

        [ProtoMember(2)]
        public TestDto Data { get; set; }
    }
}