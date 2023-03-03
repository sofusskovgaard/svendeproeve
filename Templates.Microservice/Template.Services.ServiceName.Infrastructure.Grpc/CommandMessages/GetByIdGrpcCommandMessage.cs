using App.Common.Grpc;
using ProtoBuf;

namespace Template.Services.ServiceName.Infrastructure.Grpc.CommandMessages;

[ProtoContract]
public class GetByIdGrpcCommandMessage : IGrpcCommandMessage
{
    [ProtoMember(1)]
    public string Id { get; set; }
}