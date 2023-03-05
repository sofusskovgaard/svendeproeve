using App.Common.Grpc;
using App.Data.Services;
using App.Infrastructure.Grpc;
using App.Services.Tickets.Common.Dtos;
using App.Services.Tickets.Data.Entities;
using App.Services.Tickets.Infrastructure.Commands;
using App.Services.Tickets.Infrastructure.Grpc;
using App.Services.Tickets.Infrastructure.Grpc.CommandMessages;
using App.Services.Tickets.Infrastructure.Grpc.CommandResults;
using AutoMapper;
using MassTransit;

namespace App.Services.Tickets.Infrastructure
{
    public class TicketGrpcService : BaseGrpcService, ITicketGrpcService
    {
        private readonly ITicketService _ticketService;

        private readonly IEntityDataService _entityDataService;

        private readonly IMapper _mapper;

        private readonly IPublishEndpoint _publishEndpoint;

        public TicketGrpcService(IEntityDataService entityDataService, IMapper mapper, IPublishEndpoint publishEndpoint, ITicketService ticketService)
        {
            _entityDataService = entityDataService;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
            _ticketService = ticketService;
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
                await _ticketService.CreateTickets(new BookTicketsCommandMessage
                {
                    UserId = message.Metadata.UserId,
                    TicketOrders = message.TicketOrders.Select((x) => new BookTicketsCommandMessage.TicketOrder { ProductId = x.ProductId, Recipient = x.Recipient }).ToArray()
                });

                return new BookTicketsGrpcCommandResult
                {
                    Metadata = new GrpcCommandResultMetadata{ Success = true }
                };
            });
        }
    }
}