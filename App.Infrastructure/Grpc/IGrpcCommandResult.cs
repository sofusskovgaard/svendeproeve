namespace App.Infrastructure.Grpc;

public interface IGrpcCommandResult
{
    GrpcCommandResultMetadata Metadata { get; set; }
}