﻿using App.Infrastructure.Grpc;
using ProtoBuf;

namespace App.Services.Teams.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class GetTeamsByMemberIdCommandMessage : IGrpcCommandMessage
    {
        [ProtoMember(1)]
        public string MemberId { get; set; }
    }
}
