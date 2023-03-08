using ProtoBuf;

namespace App.Common.Grpc;

/// <inheritdoc />
[ProtoContract]
public class GrpcCommandResultMetadata : IGrpcCommandResultMetadata
{
    /// <inheritdoc />
    [ProtoMember(1)]
    public bool Success { get; set; }

    /// <inheritdoc />
    [ProtoMember(2)]
    public string? Message { get; set; }

    /// <inheritdoc />
    [ProtoMember(3)]
    public string[] Errors { get; set; } = {};
}