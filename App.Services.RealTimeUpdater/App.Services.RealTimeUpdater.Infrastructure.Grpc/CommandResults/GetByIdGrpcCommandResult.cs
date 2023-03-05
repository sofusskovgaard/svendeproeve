using App.Common.Grpc;
using App.Services.RealTimeUpdater.Common.Test;
using ProtoBuf;

namespace App.Services.RealTimeUpdater.Infrastructure.Grpc.CommandResults
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