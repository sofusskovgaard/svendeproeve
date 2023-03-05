using App.Services.Billing.Infrastructure.Grpc;
using App.Services.Billing.Infrastructure.Grpc.CommandMessages;
using App.Services.Gateway.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using App.Services.Gateway.Common;

namespace App.Services.Gateway.Controllers
{
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class BillingController : ApiController
    {
        private readonly IBillingGrpcService _billingGrpcservice;

        public BillingController(IBillingGrpcService billingGrpcservice)
        {
            _billingGrpcservice = billingGrpcservice;
        }

        [HttpGet]
        [Route("{id}")]
        public Task<IActionResult> GetBillingById(string id)
        {
            return TryAsync(() => _billingGrpcservice.GetBillingById(new GetBillingByIdGrpcCommandMessage { Id = id }));
        }

        [HttpPost]
        [Route("")]
        public Task<IActionResult> CreateBilling(CreateBillingModel model)
        {
            return TryAsync(() =>
            {
                var command = new CreateBillingGrpcCommandMessage
                {
                    OrderId = model.OrderId
                };

                return _billingGrpcservice.CreateBilling(command);
            });
        }
    }
}
