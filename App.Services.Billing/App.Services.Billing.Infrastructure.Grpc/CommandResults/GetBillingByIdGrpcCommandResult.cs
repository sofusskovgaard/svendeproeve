using App.Infrastructure.Grpc;
using App.Services.Billing.Common.Dtos;
using ProtoBuf;

namespace App.Services.Billing.Infrastructure.Grpc.CommandResults
{
    [ProtoContract]
    public class GetBillingByIdGrpcCommandResult : IGrpcCommandResult
    {
        [ProtoMember(1)]
        public GrpcCommandResultMetadata Metadata { get; set; }

        [ProtoMember(2)]
        public BillingDto Billing { get; set; }
    }
}