using App.Common.Grpc;
using App.Services.Gateway.Common;
using App.Services.Gateway.Infrastructure;
using App.Services.Orders.Infrastructure.Grpc;
using App.Services.Orders.Infrastructure.Grpc.CommandMessages;
using App.Services.Orders.Infrastructure.Grpc.CommandResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace App.Services.Gateway.Controllers;

[Route("api/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
public class OrdersController : ApiController
{
    private readonly IOrdersGrpcService _ordersGrpcService;

    public OrdersController(IOrdersGrpcService ordersGrpcService)
    {
        _ordersGrpcService = ordersGrpcService;
    }

    /// <summary>
    ///     Get all orders
    /// </summary>
    /// <param name="userId">if specified will return orders for user</param>
    /// <returns></returns>
    [HttpGet]
    [Route(""), Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetOrdersGrpcCommandResult))]
    public Task<IActionResult> GetOrders(string? userId = null)
    {
        return TryAsync(() => _ordersGrpcService.GetOrders(CreateCommandMessage<GetOrdersGrpcCommandMessage>(message => message.UserId = userId)));
    }

    /// <summary>
    ///     Get order by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{id}"), Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetOrderByIdGrpcCommandResult))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(IGrpcCommandResult))]
    public Task<IActionResult> GetOrderById(string id)
    {
        return TryAsync(() => _ordersGrpcService.GetOrderById(CreateCommandMessage<GetOrderByIdGrpcCommandMessage>(message => message.Id = id)));
    }
}