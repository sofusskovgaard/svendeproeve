using App.Infrastructure.Grpc;
using ProtoBuf;

namespace App.Services.Games.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class CreateGameGrpcCommandMessage : IGrpcCommandMessage
    {
        [ProtoMember(1)]
        public string Name { get; set; }
        [ProtoMember(2)]
        public string Discription { get; set; }
        [ProtoMember(3)]
        public string ProfilePicture { get; set; }
        [ProtoMember(4)]
        public string CoverPicture { get; set; }
        [ProtoMember(5)]
        public string[] Genre { get; set; }
    }
}
