﻿using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Orders.Infrastructure.Grpc.CommandMessages;

[ProtoContract]
public class GetProductsGrpcCommandMessage : GrpcCommandMessage
{
    [ProtoMember(100)]
    public override GrpcCommandMessageMetadata? Metadata { get; set; }
}