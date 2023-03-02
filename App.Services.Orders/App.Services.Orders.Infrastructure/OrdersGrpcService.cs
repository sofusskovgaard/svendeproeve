using App.Data.Services;
using App.Infrastructure.Grpc;
using App.Services.Orders.Common.Dtos;
using App.Services.Orders.Data.Entities;
using App.Services.Orders.Infrastructure.Grpc;
using App.Services.Orders.Infrastructure.Grpc.CommandMessages;
using App.Services.Orders.Infrastructure.Grpc.CommandResults;
using AutoMapper;
using MassTransit;
using ProtoBuf.Grpc.Configuration;

namespace App.Services.Orders.Infrastructure
{
    public class OrdersGrpcService : BaseGrpcService, IOrdersGrpcService
    {
        private readonly IEntityDataService _entityDataService;

        private readonly IMapper _mapper;

        private readonly IPublishEndpoint _publishEndpoint;

        public OrdersGrpcService(IEntityDataService entityDataService, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _entityDataService = entityDataService;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        public ValueTask<GetOrderByIdGrpcCommandResult> GetOrderById(GetOrderByIdGrpcCommandMessage message)
        {
            return TryAsync(async () =>
            {
                var order = await _entityDataService.GetEntity<OrderEntity>(message.Id);

                return new GetOrderByIdGrpcCommandResult
                {
                    Metadata = new GrpcCommandResultMetadata
                    {
                        Success = true
                    },
                    Order = _mapper.Map<OrderDto>(order)
                };
            });
        }

        //public ValueTask<CreateOrderGrpcCommandResult> CreateOrder(CreateOrderGrpcCommandMessage message)
        //{
        //    return TryAsync(async () =>
        //    {
        //        var order = new OrderEntity
        //        {
        //            UserId = message.UserId,
        //            Total = message.Total,
        //            TicketIds = message.TicketIds
        //        };

        //        await _entityDataService.SaveEntity(order);
                
        //        var dto = _mapper.Map<OrderDto>(order);

        //        return new CreateOrderGrpcCommandResult
        //        {
        //            Metadata = new GrpcCommandResultMetadata
        //            {
        //                Success = true
        //            },
        //            Order = dto
        //        };
        //    });
        //}

        public ValueTask<GetProductByIdGrpcCommandResult> GetProductById(GetProductByIdGrpcCommandMessage message)
        {
            return TryAsync(async () =>
            {
                var order = await _entityDataService.GetEntity<ProductEntity>(message.Id);

                return new GetProductByIdGrpcCommandResult
                {
                    Metadata = new GrpcCommandResultMetadata
                    {
                        Success = true
                    },
                    Product = _mapper.Map<ProductDto>(order)
                };
            });
        }

        public ValueTask<CreateProductGrpcCommandResult> CreateProduct(CreateProductGrpcCommandMessage message)
        {
            return TryAsync(async () =>
            {
                var product = new ProductEntity
                {
                    Name = message.Name,
                    Description = message.Description,
                    Price = message.Price,
                    ReferenceId = message.ReferenceId,
                    ReferenceType = message.ReferenceType
                };

                await _entityDataService.SaveEntity(product);

                var dto = _mapper.Map<ProductDto>(product);

                return new CreateProductGrpcCommandResult
                {
                    Metadata = new GrpcCommandResultMetadata
                    {
                        Success = true
                    },
                    Product = dto
                };
            });
        }
    }
}