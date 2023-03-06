using ProtoBuf;
using App.Common.Grpc;

namespace App.Services.Orders.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class CreateProductGrpcCommandMessage : GrpcCommandMessage
    {
        [ProtoMember(1)]
        public string Name { get; set; }

        [ProtoMember(2)]
        public string Description { get; set; }

        [ProtoMember(3)]
        public decimal Price { get; set; }

        [ProtoMember(4)]
        public string? ReferenceId { get; set; }

        [ProtoMember(5)]
        public string? ReferenceType { get; set; }

        [ProtoMember(100)]
        public override GrpcCommandMessageMetadata? Metadata { get; set; }
    }
}
