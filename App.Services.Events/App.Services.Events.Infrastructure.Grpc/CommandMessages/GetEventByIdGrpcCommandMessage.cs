﻿using ProtoBuf;
using App.Common.Grpc;

namespace App.Services.Events.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class GetEventByIdGrpcCommandMessage : GrpcCommandMessage
    {
        [ProtoMember(1)]
        public string Id { get; set; }

        [ProtoMember(100)]
        public override GrpcCommandMessageMetadata? Metadata { get; set; }
    }
}
