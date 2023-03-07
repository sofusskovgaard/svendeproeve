﻿using App.Common.Grpc;
using App.Services.Tournaments.Common.Dtos;
using ProtoBuf;

namespace App.Services.Tournaments.Infrastructure.Grpc.CommandResults;

[ProtoContract]
public class GetMatchesByTeamIdGrpcCommandResult : IGrpcCommandResult
{
    [ProtoMember(2)]
    public IEnumerable<MatchDto> MatchDtos { get; set; }

    [ProtoMember(1)]
    public GrpcCommandResultMetadata Metadata { get; set; }
}