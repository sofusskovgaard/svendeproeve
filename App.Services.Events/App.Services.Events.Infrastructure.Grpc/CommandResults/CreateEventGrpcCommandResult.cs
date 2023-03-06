﻿using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Events.Infrastructure.Grpc.CommandResults;

[ProtoContract]
public class CreateEventGrpcCommandResult : IGrpcCommandResult
{
    [ProtoMember(1)]
    public GrpcCommandResultMetadata Metadata { get; set; }
}