using App.Infrastructure.Events;

namespace App.Services.Orders.Infrastructure.Events;

public class ProductCreatedEventMessage: IEventMessage
{
    public string ProductId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }
}
