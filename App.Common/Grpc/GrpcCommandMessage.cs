namespace App.Common.Grpc;

/// <inheritdoc />
public abstract class GrpcCommandMessage : IGrpcCommandMessage
{
    /// <inheritdoc />
    public abstract GrpcCommandMessageMetadata? Metadata { get; set; }
}