using App.Services.Departments.Infrastructure.Grpc;
using App.Services.Departments.Infrastructure.Grpc.CommandMessages;
using App.Services.Gateway.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace App.Services.Gateway.Controllers
{
    [Route("api/[controller]")]
    public class DepartmentsController : ApiController
    {
        private readonly IDepartmentsGrpcService _departmentsGrpcService;
        public DepartmentsController(IDepartmentsGrpcService departmentsGrpcService)
        {
            _departmentsGrpcService = departmentsGrpcService;
        }

        [HttpPost]
        [Route("create")]
        public Task<IActionResult> CreateDepartment([FromBody] CreateDepartemtnModel model)
        {
            return TryAsync(() =>
            {
                var command = new CreateDepartmentCommandMessage
                {
                    Name = model.Name,
                    Address = model.Address,
                    OrganizationIds = model.OrganizationIds
                };

                return _departmentsGrpcService.CreateDepartment(command);
            });
        }
    }

    public record CreateDepartemtnModel(string Name, string Address, string[] OrganizationIds);
}
