﻿using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Authentication.Infrastructure.Grpc.CommandMessages;

[ProtoContract]
public class CheckEmailAvailabilityGrpcCommandMessage : GrpcCommandMessage
{
    [ProtoMember(1)]
    public string Email { get; set; }

    [ProtoMember(100)]
    public override GrpcCommandMessageMetadata? Metadata { get; set; }
}