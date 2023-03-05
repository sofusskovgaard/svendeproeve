using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Games.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class CreateGameGrpcCommandMessage : GrpcCommandMessage
    {
        [ProtoMember(1)]
        public string Name { get; set; }

        [ProtoMember(2)]
        public string Description { get; set; }

        [ProtoMember(3)]
        public string ProfilePicture { get; set; }

        [ProtoMember(4)]
        public string CoverPicture { get; set; }

        [ProtoMember(5)]
        public string[] Genre { get; set; }

        [ProtoMember(100)]
        public override GrpcCommandMessageMetadata? Metadata { get; set; }
    }
}
