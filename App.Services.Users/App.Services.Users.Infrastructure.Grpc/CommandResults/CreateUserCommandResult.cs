using App.Infrastructure.Grpc;
using App.Services.Users.Common.Dtos;
using ProtoBuf;

namespace App.Services.Users.Infrastructure.Grpc.CommandResults;

[ProtoContract]
public class CreateUserCommandResult : IGrpcCommandResult
{
    [ProtoMember(1)]
    public GrpcCommandResultMetadata Metadata { get; set; }

    [ProtoMember(2)]
    public UserDetailedDto User { get; set; }
}