using App.Services.Organizations.Common.Dtos;
using ProtoBuf;
using App.Common.Grpc;

namespace App.Services.Organizations.Infrastructure.Grpc.CommandResults
{
    [ProtoContract]
    public class GetOrganizationByIdGrpcCommandResult : IGrpcCommandResult
    {
        [ProtoMember(1)]
        public GrpcCommandResultMetadata Metadata { get; set; }
        [ProtoMember(2)]
        public OrganizationDto Organization { get; set; }
    }
}
