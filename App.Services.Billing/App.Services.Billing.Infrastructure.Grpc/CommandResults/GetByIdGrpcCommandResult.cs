using App.Infrastructure.Grpc;
using App.Services.Billing.Common.Test;
using ProtoBuf;

namespace App.Services.Billing.Infrastructure.Grpc.CommandResults
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