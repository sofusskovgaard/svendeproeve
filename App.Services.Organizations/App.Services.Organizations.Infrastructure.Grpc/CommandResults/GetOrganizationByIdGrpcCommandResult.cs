using App.Common.Grpc;
using App.Services.Organizations.Common.Dtos;
using ProtoBuf;

namespace App.Services.Organizations.Infrastructure.Grpc.CommandResults;

[ProtoContract]
public class GetOrganizationByIdGrpcCommandResult : IGrpcCommandResult
{
    [ProtoMember(2)]
    public OrganizationDto Organization { get; set; }

    [ProtoMember(1)]
    public GrpcCommandResultMetadata Metadata { get; set; }
}