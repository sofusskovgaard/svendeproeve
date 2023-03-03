namespace App.Common.Grpc;

public interface IGrpcCommandResult
{
    GrpcCommandResultMetadata Metadata { get; set; }
}