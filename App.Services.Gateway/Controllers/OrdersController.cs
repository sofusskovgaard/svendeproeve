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

        [HttpGet]
        [Route("{id}/pay")]
        public Task<IActionResult> PayOrderById(string id)
        {
            return TryAsync(() => _ordersGrpcService.GetOrderById(new GetOrderByIdGrpcCommandMessage { Id = id }));
        }

        //[HttpPost]
        //public Task<IActionResult> CreateOrder([FromBody] CreateOrderModel model)
        //{
        //    return TryAsync(() =>
        //    {
        //        var command = new CreateOrderGrpcCommandMessage
        //        {
        //            UserId  = model.UserId,
        //            Total = model.Total,
        //            TicketIds = model.TicketIds,
        //        };

        //         return _ordersGrpcService.CreateOrder(command);
        //    });
        //}

        [HttpGet]
        [Route("product/{id}")]
        public Task<IActionResult> GetProductById(string id)
        {
            return TryAsync(() => _ordersGrpcService.GetProductById(new GetProductByIdGrpcCommandMessage { Id = id }));
        }

        [HttpPost]
        [Route("product")]
        public Task<IActionResult> CreateProduct([FromBody] CreateProductModel model)
        {
            return TryAsync(() =>
            {
                var command = new CreateProductGrpcCommandMessage
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    ReferenceId = model.ReferenceId,
                    ReferenceType = model.ReferenceType
                };

                return _ordersGrpcService.CreateProduct(command);
            });
        }
    }
    //public record CreateOrderModel(string UserId, decimal Total, string[] TicketIds);
    public record CreateProductModel(string Name, string Description, decimal Price, string? ReferenceId, string? ReferenceType);
}
