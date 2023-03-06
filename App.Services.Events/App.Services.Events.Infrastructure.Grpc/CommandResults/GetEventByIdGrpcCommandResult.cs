﻿using App.Common.Grpc;
using App.Services.Events.Common.Dtos;
using ProtoBuf;

namespace App.Services.Events.Infrastructure.Grpc.CommandResults;

[ProtoContract]
public class GetEventByIdGrpcCommandResult : IGrpcCommandResult
{
    [ProtoMember(2)]
    public EventDto Data { get; set; }

    [ProtoMember(1)]
    public GrpcCommandResultMetadata Metadata { get; set; }
}