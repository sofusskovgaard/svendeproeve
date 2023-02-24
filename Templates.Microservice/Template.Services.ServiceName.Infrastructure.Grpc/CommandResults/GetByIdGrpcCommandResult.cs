using App.Infrastructure.Grpc;
using ProtoBuf;
using Template.Services.ServiceName.Common.Test;

namespace Template.Services.ServiceName.Infrastructure.Grpc.CommandResults;

[ProtoContract]
public class GetByIdGrpcCommandResult : IGrpcCommandResult
{
    [ProtoMember(1)]
    public GrpcCommandResultMetadata Metadata { get; set; }

    [ProtoMember(2)]
    public TestDto Data { get; set; }
}