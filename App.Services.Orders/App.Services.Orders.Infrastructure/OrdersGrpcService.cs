using App.Common.Grpc;
using App.Data.Services;
using App.Infrastructure.Grpc;
using App.Services.Orders.Common.Dtos;
using App.Services.Orders.Data.Entities;
using App.Services.Orders.Infrastructure.Commands;
using App.Services.Orders.Infrastructure.Grpc;
using App.Services.Orders.Infrastructure.Grpc.CommandMessages;
using App.Services.Orders.Infrastructure.Grpc.CommandResults;
using AutoMapper;
using MassTransit;
using MongoDB.Driver;

namespace App.Services.Orders.Infrastructure;

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

    public ValueTask<GetOrdersGrpcCommandResult> GetOrders(GetOrdersGrpcCommandMessage message)
    {
        return TryAsync(async () =>
        {
            var entities = !string.IsNullOrEmpty(message.UserId)
                ? await _entityDataService.ListEntities<OrderEntity>(filter =>
                    filter.Eq(entity => entity.UserId, message.UserId))
                : await _entityDataService.ListEntities<OrderEntity>();

            return new GetOrdersGrpcCommandResult
            {
                Metadata = new GrpcCommandResultMetadata { Success = true },
                Data = _mapper.Map<OrderDto[]>(entities)
            };
        });
    }

    public ValueTask<GetOrderByIdGrpcCommandResult> GetOrderById(GetOrderByIdGrpcCommandMessage message)
    {
        return TryAsync(async () =>
        {
            var order = await _entityDataService.GetEntity<OrderEntity>(message.Id);

            return new GetOrderByIdGrpcCommandResult
            {
                Metadata = new GrpcCommandResultMetadata{ Success = true },
                Data = _mapper.Map<OrderDto>(order)
            };
        });
    }

    public ValueTask<GetProductByIdGrpcCommandResult> GetProductById(GetProductByIdGrpcCommandMessage message)
    {
        return TryAsync(async () =>
        {
            var order = await _entityDataService.GetEntity<ProductEntity>(message.Id);

            return new GetProductByIdGrpcCommandResult
            {
                Metadata = new GrpcCommandResultMetadata{ Success = true },
                Data = _mapper.Map<ProductDto>(order)
            };
        });
    }

    public ValueTask<CreateProductGrpcCommandResult> CreateProduct(CreateProductGrpcCommandMessage message)
    {
        return TryAsync(async () =>
        {
            var createMessage = new CreateProductCommandMessage
            {
                Name = message.Name,
                Description = message.Description,
                Price = message.Price,
                ReferenceId = message.ReferenceId,
                ReferenceType = message.ReferenceType
            };

            await _publishEndpoint.Publish(createMessage);

            return new CreateProductGrpcCommandResult{ Metadata = new GrpcCommandResultMetadata { Success = true } };
        });
    }

    public ValueTask<GetProductsGrpcCommandResult> GetProducts(GetProductsGrpcCommandMessage message)
    {
        return TryAsync(async () =>
        {
            var filters = new List<FilterDefinition<ProductEntity>>();

            if (!string.IsNullOrEmpty(message.ReferenceId))
            {
                filters.Add(new FilterDefinitionBuilder<ProductEntity>().Eq(entity => entity.ReferenceId, message.ReferenceId));
            }

            if (!string.IsNullOrEmpty(message.ReferenceType))
            {
                filters.Add(new FilterDefinitionBuilder<ProductEntity>().Eq(entity => entity.ReferenceType, message.ReferenceType));
            }

            var entities = await _entityDataService.ListEntities<ProductEntity>(filter => filters.Any() ? filter.And(filters) : FilterDefinition<ProductEntity>.Empty);

            return new GetProductsGrpcCommandResult
            {
                Metadata = new GrpcCommandResultMetadata { Success = true },
                Data = _mapper.Map<ProductDto[]>(entities)
            };
        });
    }
}