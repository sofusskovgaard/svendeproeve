using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Users.Infrastructure.Grpc.CommandMessages;

[ProtoContract]
public class GetUserByIdGrpcCommandMessage : IGrpcCommandMessage
{
    public GetUserByIdGrpcCommandMessage() {}

    public GetUserByIdGrpcCommandMessage(string id)
    {
        Id = id;
    }

    [ProtoMember(1)]
    public string Id { get; set; }
}