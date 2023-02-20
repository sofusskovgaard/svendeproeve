﻿using App.Infrastructure.Grpc;
using ProtoBuf;

namespace App.Services.Users.Infrastructure.Grpc.CommandMessages;

[ProtoContract]
public class GetUserByIdCommandMessage : IGrpcCommandMessage
{
    [ProtoMember(1)]
    public string Id { get; set; }
}