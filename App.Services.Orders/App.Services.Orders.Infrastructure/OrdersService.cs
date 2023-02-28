using App.Data.Services;
using App.Services.Orders.Infrastructure.Commands;
using App.Services.Orders.Infrastructure.Grpc.CommandMessages;
using App.Services.Tickets.Infrastructure.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public ValueTask TicketsBooked(TicketsBookedEventMessage message)
        {
            CreateOrder(new CreateOrderCommandMessage
            {
                
            });
            _publishEndpoint.Publish()
            throw new NotImplementedException();
        }
    }
}
