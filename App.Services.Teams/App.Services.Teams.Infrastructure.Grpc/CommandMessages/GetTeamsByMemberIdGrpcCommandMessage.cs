using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Teams.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class GetTeamsByMemberIdGrpcCommandMessage : IGrpcCommandMessage
    {
        [ProtoMember(1)]
        public string MemberId { get; set; }
    }
}
