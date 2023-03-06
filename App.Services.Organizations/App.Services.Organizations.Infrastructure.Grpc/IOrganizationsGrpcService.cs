using App.Services.Organizations.Infrastructure.Grpc.CommandMessages;
using App.Services.Organizations.Infrastructure.Grpc.CommandResults;
using ProtoBuf.Grpc.Configuration;

namespace App.Services.Organizations.Infrastructure.Grpc;

[Service("app.services.organizations")]
public interface IOrganizationsGrpcService
{
    [Operation]
    ValueTask<GetOrganizationByIdGrpcCommandResult> GetOrganizationById(GetOrganizationByIdGrpcCommandMessage message);

    [Operation]
    ValueTask<CreateOrganizationGrpcCommandResult> CreateOrganization(CreateOrganizationGrpcCommandMessage message);

    [Operation]
    ValueTask<UpdateOrganizationGrpcCommandResult> UpdateOrganization(UpdateOrganizationGrpcCommandMessage message);

    [Operation]
    ValueTask<DeleteOrganizationGrpcCommandResult> DeleteOrganization(DeleteOrganizationGrpcCommandMessage message);

    [Operation]
    ValueTask<GetOrganizationsGrpcCommandResult> GetOrganizations(GetOrganizationsGrpcCommandMessage message);
}