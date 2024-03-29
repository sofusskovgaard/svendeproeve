﻿using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Authentication.Infrastructure.Grpc.CommandMessages;

[ProtoContract]
public class LoginGrpcCommandMessage : GrpcCommandMessage
{
    [ProtoMember(1)]
    public string Username { get; set; }

    [ProtoMember(2)]
    public string Password { get; set; }

    [ProtoMember(3)]
    public string? IP { get; set; } = null;

    [ProtoMember(4)]
    public string? UserAgent { get; set; } = null;

    [ProtoMember(100)]
    public override GrpcCommandMessageMetadata? Metadata { get; set; }
}