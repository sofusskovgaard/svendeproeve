using App.Services.Billing.Common.Dtos;
using ProtoBuf;
using App.Common.Grpc;

namespace App.Services.Billing.Infrastructure.Grpc.CommandResults
{
    [ProtoContract]
    public class CreateBillingGrpcCommandResult : IGrpcCommandResult
    {
        [ProtoMember(1)]
        public GrpcCommandResultMetadata Metadata { get; set; }

        [ProtoMember(2)]
        public BillingDto Billing { get; set; }
    }
}
