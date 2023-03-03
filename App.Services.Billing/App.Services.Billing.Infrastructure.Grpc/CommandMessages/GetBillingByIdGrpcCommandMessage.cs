using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Billing.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class GetBillingByIdGrpcCommandMessage : IGrpcCommandMessage
    {
        [ProtoMember(1)]
        public string Id { get; set; }
    }
}