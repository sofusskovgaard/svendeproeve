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

        public ValueTask<CreateOrderGrpcCommandResult> CreateOrder(CreateOrderGrpcCommandMessage message)
        {
            return TryAsync(async () =>
            {
                var Order = new OrderEntity
                {
                    UserId = message.UserId,
                    Total = message.Total,
                    TicketIds = message.TicketIds
                };

                await _entityDataService.SaveEntity(Order);
                
                var dto = _mapper.Map<OrderDto>(Order);

                return new CreateOrderGrpcCommandResult
                {
                    Metadata = new GrpcCommandResultMetadata
                    {
                        Success = true
                    },
                    Order = dto
                };
            });
        }
    }
}