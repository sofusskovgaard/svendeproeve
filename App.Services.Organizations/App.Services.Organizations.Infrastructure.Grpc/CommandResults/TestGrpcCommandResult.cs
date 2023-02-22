using App.Infrastructure.Grpc;

namespace App.Services.Organizations.Infrastructure.Grpc.CommandResults
{
    public class TestGrpcCommandResult : IGrpcCommandResult
    {
        public GrpcCommandResultMetadata Metadata { get; set; }
    }
}