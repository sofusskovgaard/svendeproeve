using App.Infrastructure.Grpc;
using App.Services.Users.Common.Dtos;
using ProtoBuf;

namespace App.Services.Users.Infrastructure.Grpc.CommandResults;

[ProtoContract]
public class GetUsersInTeamGrpcCommandResult : IGrpcCommandResult
{
    [ProtoMember(1)]
    public GrpcCommandResultMetadata Metadata { get; set; }

    [ProtoMember(2)]
    public UserDto[] Users { get; set; }
}