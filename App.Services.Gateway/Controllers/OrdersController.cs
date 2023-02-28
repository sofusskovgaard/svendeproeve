using App.Services.Gateway.Infrastructure;
using App.Services.Orders.Infrastructure.Grpc;
using App.Services.Orders.Infrastructure.Grpc.CommandMessages;
using Microsoft.AspNetCore.Mvc;

namespace App.Services.Gateway.Controllers
{
    [Route("api/[controller]")]
    public class OrdersController : ApiController
    {
        private readonly IOrdersGrpcService _ordersGrpcService;

        public OrdersController(IOrdersGrpcService ordersGrpcService)
        {
            _ordersGrpcService = ordersGrpcService;
        }

        [HttpGet]
        [Route("{id}")]
        public Task<IActionResult> GetOrderById(string id)
        {
            return TryAsync(() => _ordersGrpcService.GetOrderById(new GetOrderByIdGrpcCommandMessage { Id = id}));
        }
        [HttpPost]
        public Task<IActionResult> CreateOrder([FromBody] CreateOrderModel model)
        {
            return TryAsync(() =>
            {
                var command = new CreateOrderGrpcCommandMessage
                {
                    UserId  = model.UserId,
                    Total = model.Total,
                    TicketIds = model.TicketIds,
                };

                 return _ordersGrpcService.CreateOrder(command);
            });
        }
    }
    public record CreateOrderModel(string UserId, double Total, string[] TicketIds);
}
