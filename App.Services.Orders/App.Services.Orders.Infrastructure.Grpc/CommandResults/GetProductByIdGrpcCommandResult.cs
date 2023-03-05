using App.Services.Orders.Common.Dtos;
using ProtoBuf;
using App.Common.Grpc;

namespace App.Services.Orders.Infrastructure.Grpc
{
    [ProtoContract]
    public class GetProductByIdGrpcCommandResult : IGrpcCommandResult
    {
        [ProtoMember(1)]
        public GrpcCommandResultMetadata Metadata { get; set; }

        [ProtoMember(2)]
        public ProductDto Product { get; set; }
    }
}
