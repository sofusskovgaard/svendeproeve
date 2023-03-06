using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Teams.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class GetTeamsByMemberIdGrpcCommandMessage : GrpcCommandMessage
    {
        [ProtoMember(1)]
        public string MemberId { get; set; }

        [ProtoMember(100)]
        public override GrpcCommandMessageMetadata? Metadata { get; set; }
    }
}
