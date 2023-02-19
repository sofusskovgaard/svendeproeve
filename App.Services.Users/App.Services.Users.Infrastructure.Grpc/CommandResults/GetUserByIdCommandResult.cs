using App.Infrastructure.Grpc;
using ProtoBuf;

namespace App.Services.Users.Infrastructure.Grpc.CommandResults;

[ProtoContract]
public class GetUserByIdCommandResult : IGrpcCommandResult
{
    [ProtoMember(0)]
    public GrpcCommandResultMetadata Metadata { get; set; }
}