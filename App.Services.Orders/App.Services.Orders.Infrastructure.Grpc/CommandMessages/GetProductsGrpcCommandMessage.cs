using ProtoBuf;
using App.Common.Grpc;

namespace App.Services.Orders.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class GetProductsGrpcCommandMessage : IGrpcCommandMessage
    {

    }
}
