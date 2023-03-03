using App.Services.Departments.Common.Dtos;
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

        /// <summary>
        /// Get all departments
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Task<IActionResult> GetAllDepartments()
        {
            return TryAsync(() => this._departmentsGrpcService.GetAllDepartments(new GetAllDepartmentsGrpcCommandMessage()));
        }

        /// <summary>
        /// Get deparments with the same name as given
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{name}/name")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Task<IActionResult> GetDepartmentsByName(string name)
        {
            return TryAsync(() => this._departmentsGrpcService.GetDepartmentsByName(new GetDepartmentsByNameGrpcCommandMessage() { Name = name }));
        }

        /// <summary>
        /// Get departments by organization id
        /// </summary>
        /// <param name="organizationid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{organizationid}/organization")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Task<IActionResult> GetDepartmentsByOrganizationId(string organizationid)
        {
            return TryAsync(() => this._departmentsGrpcService.GetDepartmentsByOrganizationId(new GetDepartmentsByOrganizationIdGrpcCommandMessage() { OrganizationId = organizationid }));
        }

        /// <summary>
        /// Get department by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Task<IActionResult> GetDepartmentById(string id)
        {
            return TryAsync(() => this._departmentsGrpcService.GetDepartmentById(new GetDepartmentByIdGrpcCommandMessage() { Id = id }));
        }

        /// <summary>
        /// Create a department
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<IActionResult> CreateDepartment([FromBody] CreateDepartmentModel model)
        {
            return TryAsync(() =>
            {
                var command = new CreateDepartmentGrpcCommandMessage
                {
                    Name = model.Name,
                    Address = model.Address,
                };

                return this._departmentsGrpcService.CreateDepartment(command);
            });
        }

        /// <summary>
        /// Update a department
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<IActionResult> UpdateDepartment([FromBody] UpdateDepartmentModel model)
        {
            return TryAsync(() => this._departmentsGrpcService.UpdateDepartment(new UpdateDepartmentGrpcCommandMessage() 
            { 
                DepartmentDto = new DepartmentDto()
                {
                    Id = model.Id,
                    Name = model.Name,
                    Address = model.Address,
                }
            }));
        }

        /// <summary>
        /// Delete a department
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<IActionResult> DeleteDepartmentById(string id)
        {
            return TryAsync(() => this._departmentsGrpcService.DeleteDepartmentById(new DeleteDepartmentByIdGrpcCommandMessage() { Id = id }));
        }
    }

    /// <summary>
    /// Data required to create a department
    /// </summary>
    /// <param name="Name"></param>
    /// <param name="Address"></param>
    public record CreateDepartmentModel(string Name, string Address);

    /// <summary>
    /// Data required to update a department
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Name"></param>
    /// <param name="Address"></param>
    public record UpdateDepartmentModel(string Id, string Name, string Address);
}
