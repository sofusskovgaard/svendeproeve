﻿using App.Common.Grpc;
using App.Services.Users.Common.Dtos;
using ProtoBuf;

namespace App.Services.Users.Infrastructure.Grpc.CommandResults;

[ProtoContract]
public class CreateUserGrpcCommandResult : IGrpcCommandResult
{
    [ProtoMember(1)]
    public GrpcCommandResultMetadata Metadata { get; set; }

    [ProtoMember(2)]
    public UserDetailedDto Data { get; set; }
}