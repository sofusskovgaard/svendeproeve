using ProtoBuf;
using App.Common.Grpc;

namespace App.Services.Events.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class UpdateEventGrpcCommandMessage : GrpcCommandMessage
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

        [ProtoMember(100)]
        public override GrpcCommandMessageMetadata? Metadata { get; set; }
    }
}
