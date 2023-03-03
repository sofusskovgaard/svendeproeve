using App.Services.Orders.Infrastructure.Grpc.CommandMessages;
using App.Services.Orders.Infrastructure.Grpc.CommandResults;
using ProtoBuf.Grpc.Configuration;

namespace App.Services.Orders.Infrastructure.Grpc
{
    [Service("app.services.orders")]
    public interface IOrdersGrpcService
    {
        [Operation]
        ValueTask<GetOrderByIdGrpcCommandResult> GetOrderById(GetOrderByIdGrpcCommandMessage message);

        //[Operation]
        //ValueTask<CreateOrderGrpcCommandResult> CreateOrder(CreateOrderGrpcCommandMessage message);

        [Operation]
        ValueTask<CreateProductGrpcCommandResult> CreateProduct(CreateProductGrpcCommandMessage message);

        [Operation]
        ValueTask<GetProductByIdGrpcCommandResult> GetProductById(GetProductByIdGrpcCommandMessage message);

        [Operation]
        ValueTask<GetProductsGrpcCommandResult> GetProducts(GetProductsGrpcCommandMessage message);
    }
}