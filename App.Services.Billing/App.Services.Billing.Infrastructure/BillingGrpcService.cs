using App.Common.Grpc;
using App.Data.Services;
using App.Infrastructure.Grpc;
using App.Services.Billing.Common.Dtos;
using App.Services.Billing.Data.Entities;
using App.Services.Billing.Infrastructure.Grpc;
using App.Services.Billing.Infrastructure.Grpc.CommandMessages;
using App.Services.Billing.Infrastructure.Grpc.CommandResults;
using AutoMapper;
using MassTransit;

namespace App.Services.Billing.Infrastructure;

public class BillingGrpcService : BaseGrpcService, IBillingGrpcService
{
    private readonly IEntityDataService _entityDataService;

    private readonly IMapper _mapper;

    public BillingGrpcService(IEntityDataService entityDataService, IMapper mapper)
    {
        _entityDataService = entityDataService;
        _mapper = mapper;
    }

    public ValueTask<GetChargeByOrderGrpcCommandResult> GetChargeByOrder(GetChargeByOrderGrpcCommandMessage message)
    {
        return this.TryAsync(async () =>
        {
            var entities =
                await _entityDataService.ListEntities<OrderChargeEntity>(filter =>
                    filter.Eq(entity => entity.OrderId, message.OrderId));

            return new GetChargeByOrderGrpcCommandResult
            {
                Metadata = new GrpcCommandResultMetadata { Success = true },
                Data = _mapper.Map<OrderChargeDto>(entities)
            };
        });
    }

    public ValueTask<GetCardsForUserGrpcCommandResult> GetCardsForUser(GetCardsForUserGrpcCommandMessage message)
    {
        return this.TryAsync(async () =>
        {
            if (!message.Metadata.IsAdmin && !string.IsNullOrEmpty(message.Id) && message.Id != message.Metadata.UserId)
            {
                throw new Exception("You are not authorized to access this users cards");
            }

            var entities =
                await _entityDataService.ListEntities<UserCardEntity>(filter =>
                    filter.Eq(entity => entity.UserId, message.Id ?? message.Metadata.UserId));

            return new GetCardsForUserGrpcCommandResult
            {
                Metadata = new GrpcCommandResultMetadata { Success = true },
                Data = _mapper.Map<UserCardDto[]>(entities)
            };
        });
    }
}