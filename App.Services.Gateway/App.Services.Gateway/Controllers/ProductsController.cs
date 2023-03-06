using App.Common.Grpc;
using App.Services.Gateway.Common;
using App.Services.Gateway.Infrastructure;
using App.Services.Orders.Infrastructure.Grpc;
using App.Services.Orders.Infrastructure.Grpc.CommandMessages;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using App.Services.Orders.Common.Constants;
using App.Services.Orders.Infrastructure.Grpc.CommandResults;
using Microsoft.AspNetCore.Authorization;

namespace App.Services.Gateway.Controllers;

[Route("api/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
public class ProductsController : ApiController
{
    private readonly IOrdersGrpcService _ordersGrpcService;

    public ProductsController(IOrdersGrpcService ordersGrpcService)
    {
        _ordersGrpcService = ordersGrpcService;
    }

    /// <summary>
    ///     Get all products
    /// </summary>
    /// <param name="referenceId">if specified will return products for this reference id</param>
    /// <param name="referenceType">if specified will return products for this reference type, see <see cref="ProductReferenceType"/> for possible values</param>
    /// <returns></returns>
    [HttpGet]
    [Route(""), Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetProductsGrpcCommandResult))]
    public Task<IActionResult> GetProducts(string? referenceId = null, string? referenceType = null)
    {
        return TryAsync(() => _ordersGrpcService.GetProducts(CreateCommandMessage<GetProductsGrpcCommandMessage>(
            message =>
            {
                message.ReferenceId = referenceId;
                message.ReferenceType = referenceType;
            })));
    }

    /// <summary>
    ///     Get product by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{id}"), Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetProductByIdGrpcCommandResult))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(IGrpcCommandResult))]
    public Task<IActionResult> GetProductById(string id)
    {
        return TryAsync(() => _ordersGrpcService.GetProductById(CreateCommandMessage<GetProductByIdGrpcCommandMessage>(message => message.Id = id)));
    }

    /// <summary>
    ///     Create a product
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    [Route(""), Authorize]
    [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(CreateProductGrpcCommandResult))]
    public Task<IActionResult> CreateProduct([FromBody] CreateProductModel model)
    {
        return TryAsync(() => _ordersGrpcService.CreateProduct(CreateCommandMessage<CreateProductGrpcCommandMessage>(message =>
        {
            message.Name = model.Name;
            message.Description = model.Description;
            message.Price = model.Price;
            message.ReferenceId = model.ReferenceId;
            message.ReferenceType = model.ReferenceType;
        })));
    }
}