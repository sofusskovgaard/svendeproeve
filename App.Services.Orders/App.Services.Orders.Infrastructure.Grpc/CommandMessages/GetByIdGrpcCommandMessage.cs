using App.Infrastructure.Grpc;
using ProtoBuf;

namespace App.Services.Orders.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class GetByIdGrpcCommandMessage : IGrpcCommandMessage
    {
        [ProtoMember(1)]
        public string Id { get; set; }
    }
}