namespace App.Common.Grpc;

public interface IGrpcCommandResultMetadata
{
    bool Success { get; set; }

    string? Message { get; set; }

    string[] Errors { get; set; }
}