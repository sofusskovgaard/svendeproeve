﻿using App.Common.Grpc;
using App.Services.Teams.Common.Dtos;
using ProtoBuf;

namespace App.Services.Teams.Infrastructure.Grpc.CommandResults;

[ProtoContract]
public class GetAllTeamsGrpcCommandResult : IGrpcCommandResult
{
    [ProtoMember(1)]
    public GrpcCommandResultMetadata Metadata { get; set; }

    [ProtoMember(2)]
    public IEnumerable<TeamDto> Data { get; set; }
}