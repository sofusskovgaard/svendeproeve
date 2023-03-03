using App.Common.Grpc;
using App.Services.Teams.Common.Dtos;
using ProtoBuf;

namespace App.Services.Teams.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class UpdateTeamGrpcCommandMessage : GrpcCommandMessage
    {
        [ProtoMember(1)]
        public string TeamId { get; set; }

        [ProtoMember(2)]
        public UpdateTeamDto TeamDto { get; set; }

        [ProtoMember(100)]
        public override GrpcCommandMessageMetadata? Metadata { get; set; }
    }
}
