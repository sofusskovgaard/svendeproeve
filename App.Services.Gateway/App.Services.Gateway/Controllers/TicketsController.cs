using App.Common.Grpc;
using App.Services.Gateway.Common;
using App.Services.Gateway.Infrastructure;
using App.Services.Tickets.Infrastructure.Grpc;
using App.Services.Tickets.Infrastructure.Grpc.CommandMessages;
using App.Services.Tickets.Infrastructure.Grpc.CommandResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Services.Gateway.Controllers;

[Route("api/[controller]")]
public class TicketsController : ApiController
{
    private readonly ITicketGrpcService _ticketsGrpcService;

    public TicketsController(ITicketGrpcService ticketsGrpcService)
    {
        _ticketsGrpcService = ticketsGrpcService;
    }

    /// <summary>
    ///     Get all tickets
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="orderId"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    [HttpGet]
    [Route(""), Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetTicketsGrpcCommandResult))]
    public Task<IActionResult> GetAllTickets(string? userId = null, string? orderId = null, string? status = null)
    {
        return TryAsync(() => _ticketsGrpcService.GetTickets(CreateCommandMessage<GetTicketsGrpcCommandMessage>(
            message =>
            {
                message.UserId = userId;
                message.OrderId = orderId;
                message.Status = status;
            })));
    }

    /// <summary>
    ///     Get ticket by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{id}"), Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetTicketByIdGrpcCommandResult))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(IGrpcCommandResult))]
    public Task<IActionResult> GetTicketById(string id)
    {
        return TryAsync(() =>
            _ticketsGrpcService.GetTicketById(
                CreateCommandMessage<GetTicketByIdGrpcCommandMessage>(message => message.Id = id)));
    }

    /// <summary>
    ///     Book tickets
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    [Route(""), Authorize]
    [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(BookTicketsGrpcCommandResult))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(IGrpcCommandResult))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(IGrpcCommandResult))]
    public Task<IActionResult> CreateTicket([FromBody] BookTicketsModel model)
    {
        return TryAsync(() => _ticketsGrpcService.BookTickets(
            CreateCommandMessage<BookTicketsGrpcCommandMessage>(message => message.Bookings = model.Bookings)), true);
    }
}