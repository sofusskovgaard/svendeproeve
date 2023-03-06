using ProtoBuf;
using App.Common.Grpc;

namespace App.Services.Organizations.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class GetOrganizationsGrpcCommandMessage : GrpcCommandMessage
    {
        [ProtoMember(100)]
        public override GrpcCommandMessageMetadata? Metadata { get; set; }
    }
}
