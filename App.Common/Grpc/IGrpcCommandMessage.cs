namespace App.Common.Grpc;

/// <summary>
///     gRPC Command Message definition. Should always be used when defining gRPC Command Messages.
/// </summary>
internal interface IGrpcCommandMessage
{
    /// <summary>
    ///     Metadata regarding a specific gRPC method invocation.
    /// </summary>
    GrpcCommandMessageMetadata? Metadata { get; set; }
}