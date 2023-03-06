﻿using App.Services.Gateway.Common;
using App.Services.Gateway.Infrastructure;
using App.Services.Tickets.Infrastructure.Grpc;
using App.Services.Tickets.Infrastructure.Grpc.CommandMessages;
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

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> GetTicketById(string id)
    {
        return TryAsync(() =>
            _ticketsGrpcService.GetTicketById(
                CreateCommandMessage<GetTicketByIdGrpcCommandMessage>(message => message.Id = id)));
    }

    [HttpPost]
    [Route("")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public Task<IActionResult> CreateTicket([FromBody] BookTicketsModel model)
    {
        return TryAsync(() => _ticketsGrpcService.BookTickets(
            CreateCommandMessage<BookTicketsGrpcCommandMessage>(message =>
            {
                message.TicketOrders = model.TicketOrderModels.Select(x =>
                    new BookTicketsGrpcCommandMessage.TicketOrder
                        { ProductId = x.ProductId, Recipient = x.Recipient }).ToArray();
            })), true);
    }
}