namespace App.Common.Grpc;

/// <summary>
///     Metadata sent with all gRPC command results
/// </summary>
public interface IGrpcCommandResultMetadata
{
    /// <summary>
    ///     If true the invocation succeeded
    /// </summary>
    bool Success { get; set; }

    /// <summary>
    ///     If filled out <see cref="Success"/> should be false
    /// </summary>
    string? Message { get; set; }

    /// <summary>
    ///     This will be populated with exception messages if there are multiple
    /// </summary>
    string[] Errors { get; set; }
}