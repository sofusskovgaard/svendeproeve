using ProtoBuf;
using App.Common.Grpc;

namespace App.Services.Organizations.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class GetOrganizationsByNameGrpcCommandMessage : IGrpcCommandMessage
    {
        [ProtoMember(1)]
        public string Name { get; set; }
    }
}
