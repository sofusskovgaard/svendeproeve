﻿using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Tournaments.Infrastructure.Grpc.CommandResults;

[ProtoContract]
public class DeleteMatchByIdGrpcCommandResult : IGrpcCommandResult
{
    [ProtoMember(1)]
    public GrpcCommandResultMetadata Metadata { get; set; }
}