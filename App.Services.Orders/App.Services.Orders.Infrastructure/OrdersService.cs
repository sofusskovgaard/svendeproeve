using App.Data.Services;
using App.Services.Orders.Data.Entities;
using App.Services.Orders.Infrastructure.Commands;
using App.Services.Orders.Infrastructure.Events;
using App.Services.Orders.Infrastructure.Grpc.CommandMessages;
using App.Services.Tickets.Infrastructure.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Orders.Infrastructure
{
    public interface IOrdersService
    {
        public ValueTask TicketsBooked(TicketsBookedEventMessage message);
        public ValueTask CreateProduct(CreateProductCommandMessage message);
        public ValueTask<GetProductByIdCommandResult> GetProductById(GetProductByIdCommandMessage message);
        public ValueTask CreateOrder(CreateOrderCommandMessage message);
    }

    public class OrdersService : IOrdersService
    {
        private readonly IEntityDataService _entityDataService;

        private readonly IPublishEndpoint _publishEndpoint;

        public OrdersService(IEntityDataService entityDataService, IPublishEndpoint publishEndpoint)
        {
            _entityDataService = entityDataService;
            _publishEndpoint = publishEndpoint;
        }
        public ValueTask CreateOrder(CreateOrderCommandMessage message)
        {
            throw new NotImplementedException();
        }
        public ValueTask CreateProduct(CreateProductCommandMessage message)
        {
            throw new NotImplementedException();
        }

        public ValueTask<GetProductByIdCommandResult> GetProductById(GetProductByIdCommandMessage message)
        {
            throw new NotImplementedException();
        }

        public async ValueTask TicketsBooked(TicketsBookedEventMessage message)
        {
            //Get product pricings

            await CreateOrder(new CreateOrderCommandMessage
            {
                UserId = message.UserId,
                TicketIds = message.Tickets.Select(x => x.TicketId).ToArray(),
                Total = await GetTotal(message.Tickets)
            });

            //var @eventMessage = new TicketOrderCreatedEventMessage
            //{
            //    OrderId
            //}

            _publishEndpoint.Publish(message);
            
            //publish TicketOrderCreated
        }
        private async Task<double> GetTotal(TicketsBookedEventMessage.Ticket[] tickets)
        {
            //TODO: smarter code
            double total = 0;
            foreach (var ticket in tickets)
            {
                var product = await _entityDataService.GetEntity<ProductEntity>(ticket.ProductId);
                total += product.Price;
            }
            return total;
        }
    }
}
