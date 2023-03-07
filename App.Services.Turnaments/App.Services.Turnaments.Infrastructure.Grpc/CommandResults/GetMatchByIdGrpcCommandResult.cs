﻿using App.Common.Grpc;
using App.Services.Turnaments.Common.Dtos;
using ProtoBuf;

namespace App.Services.Turnaments.Infrastructure.Grpc.CommandResults;

[ProtoContract]
public class GetMatchByIdGrpcCommandResult : IGrpcCommandResult
{
    [ProtoMember(2)]
    public MatchDto MatchDto { get; set; }

    [ProtoMember(1)]
    public GrpcCommandResultMetadata Metadata { get; set; }
}