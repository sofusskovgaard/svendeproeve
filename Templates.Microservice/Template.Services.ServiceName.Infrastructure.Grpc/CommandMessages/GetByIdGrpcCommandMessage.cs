using App.Common.Grpc;
using ProtoBuf;

namespace Template.Services.ServiceName.Infrastructure.Grpc.CommandMessages;

[ProtoContract]
public class GetByIdGrpcCommandMessage : GrpcCommandMessage
{
    [ProtoMember(1)]
    public string Id { get; set; }

    [ProtoMember(100)]
    public override GrpcCommandMessageMetadata? Metadata { get; set; }
}