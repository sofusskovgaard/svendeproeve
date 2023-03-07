using ProtoBuf;

namespace App.Common.Grpc;

/// <summary>
///     Metadata regarding a specific gRPC method invocation.
/// </summary>
[ProtoContract]
public class GrpcCommandMessageMetadata
{
    /// <summary>
    ///     Id of the currently logged in user
    /// </summary>
    [ProtoMember(1)]
    public string UserId { get; set; }

    /// <summary>
    ///     If true then the current user is an administrator, if false they're not.
    /// </summary>
    [ProtoMember(2)]
    public bool IsAdmin { get; set; }
}