﻿using App.Infrastructure.Grpc;
using ProtoBuf;

namespace App.Services.Turnaments.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class GetMatchesByTeamIdCommandMessage : IGrpcCommandMessage
    {
        [ProtoMember(1)]
        public string TeamId { get; set; }
    }
}