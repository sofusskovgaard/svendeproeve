﻿using App.Infrastructure.Grpc;
using ProtoBuf;

namespace App.Services.Teams.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class GetTeamsByGameIdCommandMessage : IGrpcCommandMessage
    {
        [ProtoMember(1)]
        public string GameId { get; set; }
    }
}
