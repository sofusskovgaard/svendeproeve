using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Users.Infrastructure.Grpc.CommandMessages;

[ProtoContract]
public class GetUserByIdGrpcCommandMessage : GrpcCommandMessage
{
    public GetUserByIdGrpcCommandMessage() {}

    public GetUserByIdGrpcCommandMessage(string id)
    {
        Id = id;
    }

    [ProtoMember(1)]
    public string Id { get; set; }

    [ProtoMember(100)]
    public override GrpcCommandMessageMetadata? Metadata { get; set; }
}