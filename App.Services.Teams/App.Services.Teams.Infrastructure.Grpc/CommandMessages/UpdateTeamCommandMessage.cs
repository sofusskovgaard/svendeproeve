using App.Infrastructure.Grpc;
using App.Services.Teams.Common.Dtos;
using ProtoBuf;

namespace App.Services.Teams.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class UpdateTeamCommandMessage : IGrpcCommandMessage
    {
        [ProtoMember(1)]
        public TeamDto TeamDto { get; set; }
    }
}
