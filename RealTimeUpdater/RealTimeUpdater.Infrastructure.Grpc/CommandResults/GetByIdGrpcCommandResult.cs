using App.Common.Grpc;
using ProtoBuf;
using RealTimeUpdater.Common.Test;

namespace RealTimeUpdater.Infrastructure.Grpc.CommandResults
{
    [ProtoContract]
    public class GetByIdGrpcCommandResult : IGrpcCommandResult
    {
        [ProtoMember(1)]
        public GrpcCommandResultMetadata Metadata { get; set; }

        [ProtoMember(2)]
        public TestDto Data { get; set; }
    }
}