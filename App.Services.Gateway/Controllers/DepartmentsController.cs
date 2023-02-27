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

        [HttpGet]
        [Route("index")]
        public Task<IActionResult> GetAllDepartments()
        {
            return TryAsync(() => this._departmentsGrpcService.GetAllDepartments(new GetAllDepartmentsCommandMessage()));
        }

        [HttpGet]
        [Route("name")]
        public Task<IActionResult> GetDepartmentsByName(string name)
        {
            return TryAsync(() => this._departmentsGrpcService.GetDepartmentsByName(new GetDepartmentsByNameCommandMessage() { Name = name }));
        }

        [HttpGet]
        [Route("organization")]
        public Task<IActionResult> GetDepartmentsByOrganizationId(string organizationId)
        {
            return TryAsync(() => this._departmentsGrpcService.GetDepartmentsByOrganizationId(new GetDepartmentsByOrganizationIdCommandMessage() { OrganizationId = organizationId }));
        }

        [HttpGet]
        [Route("{id}")]
        public Task<IActionResult> GetDepartmentById(string id)
        {
            return TryAsync(() => this._departmentsGrpcService.GetDepartmentById(new GetDepartmentByIdCommandMessage() { Id = id }));
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

                return this._departmentsGrpcService.CreateDepartment(command);
            });
        }
    }

    public record CreateDepartemtnModel(string Name, string Address, string[] OrganizationIds);
}
