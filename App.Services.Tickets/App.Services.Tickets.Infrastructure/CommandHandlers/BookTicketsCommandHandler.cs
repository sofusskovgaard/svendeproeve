using App.Data.Services;
using App.Infrastructure.Commands;
using App.Services.Tickets.Data.Entities;
using App.Services.Tickets.Infrastructure.Commands;
using App.Services.Tickets.Infrastructure.Events;
using MassTransit;
using MassTransit.Transports;

namespace App.Services.Tickets.Infrastructure.CommandHandlers;

public class BookTicketsCommandHandler : ICommandHandler<BookTicketsCommandMessage>
{
    private readonly IEntityDataService _entityDataService;

    public BookTicketsCommandHandler(IEntityDataService entityDataService)
    {
        _entityDataService = entityDataService;
    }

    public async Task Consume(ConsumeContext<BookTicketsCommandMessage> context)
    {
        var message = context.Message;

        var tickets = message.Bookings.Select(ticket => new TicketEntity
        {
            BuyerId = message.UserId,
            ProductId = ticket.ProductId,
            Recipient = ticket.Recipient
        }).ToArray();

        await _entityDataService.SaveEntities(tickets);

        await context.Publish(new TicketsBookedEventMessage
        {
            UserId = message.UserId,
            Tickets = tickets.Select(x => new TicketsBookedEventMessage.Ticket { ProductId = x.ProductId, TicketId = x.Id! }).ToArray()
        });

        await context.Publish(
            new TicketStaleCheckCommandMessage { Tickets = tickets.Select(t => t.Id).ToArray() },
            context => context.Delay = TimeSpan.FromSeconds(90)
        );
    }
}