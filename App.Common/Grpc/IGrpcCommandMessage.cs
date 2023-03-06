namespace App.Common.Grpc;

public interface IGrpcCommandMessage
{
    GrpcCommandMessageMetadata? Metadata { get; set; }
}