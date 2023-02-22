using App.Infrastructure.Grpc;
using App.Services.Organizations.Infrastructure.Grpc;
using App.Services.Organizations.Infrastructure.Grpc.CommandMessages;
using App.Services.Organizations.Infrastructure.Grpc.CommandResults;
using ProtoBuf.Grpc.Configuration;

namespace App.Services.Organizations.Infrastructure
{
    public class OrganizationsGrpcService : IOrganizationsGrpcService
    {
        public Task<TestGrpcCommandResult> Test(TestGrpcCommandMessage message)
        {
            throw new NotImplementedException();
        }
        public async Task<GetOrganizationByIdCommandResult> GetOrganizationById(GetOrganizationByIdCommandMessage message)
        {
            return new GetOrganizationByIdCommandResult()
            {
                Metadata = new GrpcCommandResultMetadata()
                {
                    Success = true,
                    Message = "Oh my lord it clearly worked"
                }
            };
        }
    }
}