using ProtoBuf;
using App.Common.Grpc;

namespace App.Services.Organizations.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class GetOrganizationsByAddressGrpcCommandMessage : IGrpcCommandMessage
    {
        [ProtoMember(1)]
        public string Address { get; set; }
    }
}
