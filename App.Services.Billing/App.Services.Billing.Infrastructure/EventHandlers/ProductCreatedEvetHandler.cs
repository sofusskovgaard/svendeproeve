using App.Data.Services;
using App.Infrastructure.Events;
using App.Services.Billing.Common.Constants;
using App.Services.Billing.Data.Entities;
using App.Services.Orders.Infrastructure.Events;
using MassTransit;
using Stripe;

namespace App.Services.Billing.Infrastructure.EventHandlers;

public class ProductCreatedEvetHandler : IEventHandler<ProductCreatedEventMessage>
{
    private readonly IEntityDataService _entityDataService;

    public ProductCreatedEvetHandler(IEntityDataService entityDataService)
    {
        _entityDataService = entityDataService;
    }

    public async Task Consume(ConsumeContext<ProductCreatedEventMessage> context)
    {
        var message = context.Message;

        var productOptions = new ProductCreateOptions()
        {
            Id = message.ProductId,
            Name = message.Name,
            Description = message.Description
        };

        var productService = new ProductService();
        var product = await productService.CreateAsync(productOptions);

        var priceOptions = new PriceCreateOptions()
        {
            Currency = "DKK",
            UnitAmountDecimal = message.Price*100,
            Product = product.Id
        };

        var priceService = new PriceService();
        var price = await priceService.CreateAsync(priceOptions);

        var stripeProduct = new StripeProductEntity()
        {
            Id = product.Id,
            Prices = new[] { price.Id },
            Type = StripeProductType.OneTime
        };

        await _entityDataService.SaveEntity(stripeProduct);
    }
}