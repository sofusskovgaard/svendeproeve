using App.Infrastructure.Grpc;
using App.Services.Teams.Data.Entities;
using ProtoBuf;

namespace App.Services.Teams.Infrastructure.Grpc.CommandResults
{
    [ProtoContract]
    public class GetTeamsByMemberIdCommandResult : IGrpcCommandResult
    {
        [ProtoMember(1)]
        public GrpcCommandResultMetadata Metadata { get; set; }

        [ProtoMember(2)]
        public IEnumerable<TeamEntity> TeamsEnties { get; set; }
    }
}
