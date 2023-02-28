using App.Infrastructure.Grpc;
using App.Services.Events.Common.Dtos;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Events.Infrastructure.Grpc.CommandResults
{
    [ProtoContract]
    public class GetEventByIdGrpcCommandResult : IGrpcCommandResult
    {
        [ProtoMember(1)]
        public GrpcCommandResultMetadata Metadata { get; set; }
        [ProtoMember(2)]
        public EventDto Event { get; set; }
    }
}
