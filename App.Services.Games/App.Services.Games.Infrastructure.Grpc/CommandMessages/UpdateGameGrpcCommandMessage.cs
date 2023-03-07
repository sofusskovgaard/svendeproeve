using App.Common.Grpc;
using App.Services.Games.Common.Dtos;
using ProtoBuf;

namespace App.Services.Games.Infrastructure.Grpc.CommandMessages;

[ProtoContract]
public class UpdateGameGrpcCommandMessage : GrpcCommandMessage
{
    [ProtoMember(1)]
    public string Id { get; set; }

    [ProtoMember(2)]
    public string Name { get; set; }

    [ProtoMember(3)]
    public string Description { get; set; }

    [ProtoMember(4)]
    public string ProfilePicture { get; set; }

    [ProtoMember(5)]
    public string CoverPicture { get; set; }

    [ProtoMember(6)]
    public string[] Genre { get; set; }

    [ProtoMember(100)]
    public override GrpcCommandMessageMetadata? Metadata { get; set; }
}