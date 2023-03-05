using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Tickets.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class GetTicketByIdGrpcCommandMessage : GrpcCommandMessage
    {
        [ProtoMember(1)]
        public string Id { get; set; }

        [ProtoMember(100)]
        public override GrpcCommandMessageMetadata? Metadata { get; set; }
    }
}