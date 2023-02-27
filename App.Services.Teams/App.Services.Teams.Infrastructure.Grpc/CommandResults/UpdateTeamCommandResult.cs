using App.Infrastructure.Grpc;
using ProtoBuf;

namespace App.Services.Teams.Infrastructure.Grpc.CommandResults
{
    [ProtoContract]
    public class UpdateTeamCommandResult : IGrpcCommandResult
    {
        [ProtoMember(1)]
        public GrpcCommandResultMetadata Metadata { get; set; }
    }
}
