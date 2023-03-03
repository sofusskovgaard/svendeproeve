using App.Services.Events.Common.Dtos;
using ProtoBuf;
using App.Common.Grpc;

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
