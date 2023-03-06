namespace App.Common.Grpc;

public abstract class GrpcCommandMessage : IGrpcCommandMessage
{
    public abstract GrpcCommandMessageMetadata? Metadata { get; set; }
}