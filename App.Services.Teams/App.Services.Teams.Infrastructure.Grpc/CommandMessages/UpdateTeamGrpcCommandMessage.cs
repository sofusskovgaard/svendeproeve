using App.Infrastructure.Grpc;
using App.Services.Teams.Common.Dtos;
using ProtoBuf;

namespace App.Services.Teams.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class UpdateTeamGrpcCommandMessage : IGrpcCommandMessage
    {
        [ProtoMember(1)]
        public string TeamId { get; set; }

        [ProtoMember(2)]
        public UpdateTeamDto TeamDto { get; set; }
    }
}
