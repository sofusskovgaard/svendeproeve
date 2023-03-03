using App.Infrastructure.Events;

namespace App.Services.Orders.Infrastructure.Events
{
    public class ProductCreatedEventMessage : IEventMessage
    {
        public string Id { get; set; }

    }
}
