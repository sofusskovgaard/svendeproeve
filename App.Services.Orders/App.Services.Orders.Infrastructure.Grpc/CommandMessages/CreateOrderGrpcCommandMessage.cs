using ProtoBuf;
using App.Common.Grpc;

namespace App.Services.Orders.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class CreateOrderGrpcCommandMessage : IGrpcCommandMessage
    {
        [ProtoMember(1)]
        public string UserId { get; set; }

        [ProtoMember(2)]
        public double Total { get; set; }

        [ProtoMember(3)]
        public string[] TicketIds { get; set; }
    }
}
