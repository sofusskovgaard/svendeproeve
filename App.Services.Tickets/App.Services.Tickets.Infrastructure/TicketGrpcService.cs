using App.Data.Services;
using App.Infrastructure.Grpc;
using App.Services.Tickets.Common.Dtos;
using App.Services.Tickets.Data.Entities;
using App.Services.Tickets.Infrastructure.Grpc;
using App.Services.Tickets.Infrastructure.Grpc.CommandMessages;
using App.Services.Tickets.Infrastructure.Grpc.CommandResults;
using AutoMapper;
using MassTransit;
using ProtoBuf.Grpc.Configuration;

namespace App.Services.Tickets.Infrastructure
{
    public class TicketGrpcService : BaseGrpcService, ITicketGrpcService
    {
        private readonly IEntityDataService _entityDataService;

        private readonly IMapper _mapper;

        private readonly IPublishEndpoint _publishEndpoint;

        public TicketGrpcService(IEntityDataService entityDataService, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _entityDataService = entityDataService;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        public ValueTask<GetTicketByIdGrpcCommandResult> GetTicketById(GetTicketByIdGrpcCommandMessage message)
        {
            return TryAsync(async () =>
            {
                var ticket = await _entityDataService.GetEntity<TicketEntity>(message.Id);

                return new GetTicketByIdGrpcCommandResult
                {
                    Metadata = new GrpcCommandResultMetadata
                    {
                        Success = true
                    },
                    Ticket = _mapper.Map<TicketDto>(ticket)
                };
            });
        }

        public ValueTask<BookTicketsGrpcCommandResult> BookTickets(BookTicketsGrpcCommandMessage message)
        {
            return TryAsync(async () =>
            {
                var orderOfTickets = new List<TicketEntity>();

                foreach (var item in message.TicketOrders)
                {
                    var ticket = new TicketEntity
                    {
                        ProductId = item.ProductId,
                        Recipient = item.Recipient,
                        Status = "Created"
                    };
                    orderOfTickets.Add(ticket);
                }

                await _entityDataService.SaveEntities(orderOfTickets);

                return new BookTicketsGrpcCommandResult
                {
                    Metadata = new GrpcCommandResultMetadata
                    {
                        Success = true
                    }
                };
            });
        }
    }
}