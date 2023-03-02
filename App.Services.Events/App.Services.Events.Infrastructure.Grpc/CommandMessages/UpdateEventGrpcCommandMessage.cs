﻿using App.Infrastructure.Grpc;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Events.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class UpdateEventGrpcCommandMessage : IGrpcCommandMessage
    {
        [ProtoMember(1)]
        public string Id { get; set; }

        [ProtoMember(2)]
        public string EventName { get; set; }

        [ProtoMember(3)]
        public string Location { get; set; }

        [ProtoMember(4)]
        public DateTime StartDate { get; set; }

        [ProtoMember(5)]
        public DateTime EndDate { get; set; }
    }
}
