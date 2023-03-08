namespace App.Common.Grpc;

/// <summary>
///     gRPC command results must implement this interface
/// </summary>
public interface IGrpcCommandResult
{
    /// <summary>
    ///     Metadata sent with all gRPC command results
    /// </summary>
    GrpcCommandResultMetadata Metadata { get; set; }
}