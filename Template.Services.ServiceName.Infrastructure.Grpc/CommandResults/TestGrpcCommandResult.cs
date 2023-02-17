using App.Infrastructure.Grpc;

namespace Template.Services.ServiceName.Infrastructure.Grpc.CommandResults;

public class TestGrpcCommandResult : IGrpcCommandResult
{
    public GrpcCommandResultMetadata Metadata { get; set; }
}