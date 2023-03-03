

using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Billing.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class CreateBillingGrpcCommandMessage : IGrpcCommandMessage
    {
        [ProtoMember(1)]
        public string OrderId { get; set; }

    }
}
