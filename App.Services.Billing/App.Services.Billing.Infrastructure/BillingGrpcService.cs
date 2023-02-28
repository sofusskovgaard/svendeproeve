using App.Data.Services;
using App.Infrastructure.Grpc;
using App.Services.Billing.Common.Dtos;
using App.Services.Billing.Data.Entities;
using App.Services.Billing.Infrastructure.Grpc;
using App.Services.Billing.Infrastructure.Grpc.CommandMessages;
using App.Services.Billing.Infrastructure.Grpc.CommandResults;
using AutoMapper;
using MassTransit;
using ProtoBuf.Grpc.Configuration;

namespace App.Services.Billing.Infrastructure
{
    public class BillingGrpcService : BaseGrpcService, IBillingGrpcService
    {
        private readonly IEntityDataService _entityDataService;

        private readonly IMapper _mapper;

        private readonly IPublishEndpoint _publishEndpoint;

        public BillingGrpcService(IEntityDataService entityDataService, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _entityDataService = entityDataService;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        public ValueTask<GetBillingByIdGrpcCommandResult> GetBillingById(GetBillingByIdGrpcCommandMessage message)
        {
            return TryAsync(async () =>
            {
                var billing = await _entityDataService.GetEntity<BillingEntity>(message.Id);

                return new GetBillingByIdGrpcCommandResult
                {
                    Metadata = new GrpcCommandResultMetadata
                    {
                        Success = true
                    },
                    Billing = _mapper.Map<BillingDto>(billing)
                };
            });
        }

        public ValueTask<CreateBillingGrpcCommandResult> CreateBilling(CreateBillingGrpcCommandMessage message)
        {
            return TryAsync(async () =>
            {
                var billing = new BillingEntity
                {
                    OrderId = message.OrderId,
                    TransactionId = Guid.NewGuid().ToString(),//TODO: implement stripe for billing now only for testing
                };

                await _entityDataService.SaveEntity(billing);

                return new CreateBillingGrpcCommandResult
                {
                    Metadata = new GrpcCommandResultMetadata
                    {
                        Success = true
                    },
                    Billing = _mapper.Map<BillingDto>(billing)
                };

            });
        }
    }
}