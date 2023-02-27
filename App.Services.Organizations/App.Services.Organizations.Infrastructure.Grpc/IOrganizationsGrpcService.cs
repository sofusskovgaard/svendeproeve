using App.Services.Organizations.Infrastructure.Grpc.CommandMessages;
using App.Services.Organizations.Infrastructure.Grpc.CommandResults;
using ProtoBuf.Grpc.Configuration;

namespace App.Services.Organizations.Infrastructure.Grpc
{
    [Service("app.services.organizations")]
    public interface IOrganizationsGrpcService
    {
        [Operation]
        ValueTask<GetOrganizationByIdCommandResult> GetOrganizationById(GetOrganizationByIdCommandMessage message);
        [Operation]
        ValueTask<GetOrganizationsByAddressCommandResult> GetOrganizationsByAddress(GetOrganizationsByAddressCommandMessage message);
        [Operation]
        ValueTask<GetOrganizationsByNameCommandResult> GetOrganizationsByName(GetOrganizationsByNameCommandMessage message);
        [Operation]
        Task<CreateOrganizationCommandResult> CreateOrganization(CreateOrganizationCommandMessage message);
    }
}