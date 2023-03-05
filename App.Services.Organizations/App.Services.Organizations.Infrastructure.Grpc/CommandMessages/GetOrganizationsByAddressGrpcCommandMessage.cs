using ProtoBuf;
using App.Common.Grpc;

namespace App.Services.Organizations.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class GetOrganizationsByAddressGrpcCommandMessage : GrpcCommandMessage
    {
        [ProtoMember(1)]
        public string Address { get; set; }

        [ProtoMember(100)]
        public override GrpcCommandMessageMetadata? Metadata { get; set; }
    }
}
