﻿using App.Infrastructure.Grpc;
using ProtoBuf;

namespace App.Services.Teams.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class DeleteTeamByIdCommandMessage : IGrpcCommandMessage
    {
        [ProtoMember(1)]
        public string Id { get; set; }
    }
}