using App.Services.Organizations.Infrastructure.Grpc.CommandMessages;
using App.Services.Organizations.Infrastructure.Grpc.CommandResults;
using ProtoBuf.Grpc.Configuration;

namespace App.Services.Organizations.Infrastructure.Grpc
{
    [Service]
    public interface IOrganizationsGrpcService
    {
        Task<TestGrpcCommandResult> Test(TestGrpcCommandMessage message);
        Task<GetOrganizationByIdCommandResult> GetOrganizationById(GetOrganizationByIdCommandMessage message);
    }
}