using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Authentication.Infrastructure.Grpc.CommandResults;

[ProtoContract]
public class CheckUsernameAvailabilityGrpcCommandResult : IGrpcCommandResult
{
    [ProtoMember(1)]
    public GrpcCommandResultMetadata Metadata { get; set; }
}