using App.Infrastructure.Grpc;
using ProtoBuf.Grpc.Configuration;
using Template.Services.ServiceName.Infrastructure.Grpc;
using Template.Services.ServiceName.Infrastructure.Grpc.CommandMessages;
using Template.Services.ServiceName.Infrastructure.Grpc.CommandResults;

namespace Template.Services.ServiceName.Infrastructure;

public class ServiceNameGrpcService : BaseGrpcService, IServiceNameGrpcService
{
    public Task<GetByIdGrpcCommandResult> GetById(GetByIdGrpcCommandMessage message) =>
        this.TryAsync(() => new GetByIdGrpcCommandResult());
}