using App.Infrastructure.Events;
using App.Services.Orders.Infrastructure.Events;
using MassTransit;
using TestEventMessage = App.Services.Billing.Infrastructure.Events.TestEventMessage;

namespace App.Services.Billing.Infrastructure.EventHandlers
{
    public class TicketOrderCreatedEventHandler : IEventHandler<TicketOrderCreatedEventMessage>
    {
        public Task Consume(ConsumeContext<TicketOrderCreatedEventMessage> context)
        {
            throw new NotImplementedException();
        }
    }
}