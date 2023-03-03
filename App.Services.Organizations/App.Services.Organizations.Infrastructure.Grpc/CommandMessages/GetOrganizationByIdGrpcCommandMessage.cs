﻿using App.Infrastructure.Commands;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Organizations.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class GetOrganizationByIdGrpcCommandMessage : ICommandMessage
    {
        [ProtoMember(1)]
        public string Id { get; set; }
    }
}