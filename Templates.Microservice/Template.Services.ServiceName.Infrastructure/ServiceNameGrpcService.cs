using ProtoBuf.Grpc.Configuration;
using Template.Services.ServiceName.Infrastructure.Grpc;
using Template.Services.ServiceName.Infrastructure.Grpc.CommandMessages;
using Template.Services.ServiceName.Infrastructure.Grpc.CommandResults;

namespace Template.Services.ServiceName.Infrastructure;

public class ServiceNameGrpcService : IServiceNameGrpcService
{
    public Task<TestGrpcCommandResult> Test(TestGrpcCommandMessage message)
    {
        throw new NotImplementedException();
    }
}