using App.Infrastructure.Grpc;
using ProtoBuf;

namespace App.Services.Tickets.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class GetTicketByIdGrpcCommandMessage : IGrpcCommandMessage
    {
        [ProtoMember(1)]
        public string Id { get; set; }
    }
}