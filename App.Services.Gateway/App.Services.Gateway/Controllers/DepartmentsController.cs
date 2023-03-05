using App.Services.Departments.Common.Dtos;
using App.Services.Departments.Infrastructure.Grpc;
using App.Services.Departments.Infrastructure.Grpc.CommandMessages;
using App.Services.Gateway.Common;
using App.Services.Gateway.Infrastructure;
using App.Services.Organizations.Infrastructure.Grpc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Services.Gateway.Controllers;

[Route("api/[controller]")]
public class DepartmentsController : ApiController
{
    private readonly IDepartmentsGrpcService _departmentsGrpcService;

    public DepartmentsController(IDepartmentsGrpcService departmentsGrpcService)
    {
        this._departmentsGrpcService = departmentsGrpcService;
    }

    /// <summary>
    ///     Get all departments
    /// </summary>
    /// <param name="name">if specified will return department search by name</param>
    /// <returns></returns>
    [HttpGet]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> GetAllDepartments([FromQuery] string? name = null)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return this.TryAsync(() =>
                this._departmentsGrpcService.GetAllDepartments(
                    CreateCommandMessage<GetAllDepartmentsGrpcCommandMessage>()));
        }

        return this.TryAsync(() => this._departmentsGrpcService.GetDepartmentsByName(CreateCommandMessage<GetDepartmentsByNameGrpcCommandMessage>(message => message.Name = name)));
    }

    /// <summary>
    ///     Get department by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> GetDepartmentById(string id)
    {
        return this.TryAsync(() =>
            this._departmentsGrpcService.GetDepartmentById(CreateCommandMessage<GetDepartmentByIdGrpcCommandMessage>(message => message.Id = id)));
    }

    /// <summary>
    ///     Get organizations by department
    /// </summary>
    /// <param name="id">department id</param>
    /// <returns></returns>
    [HttpGet]
    [Route("{id}/organizations")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> GetOrganizationsByDepartment(string id)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Get events by department
    /// </summary>
    /// <param name="id">department id</param>
    /// <returns></returns>
    [HttpGet]
    [Route("{id}/events")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> GetEventsByDepartment(string id)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Create a department
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    [Route(""), Authorize]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<IActionResult> CreateDepartment([FromBody] CreateDepartmentModel model)
    {
        return this.TryAsync(() => this._departmentsGrpcService.CreateDepartment(this.CreateCommandMessage<CreateDepartmentGrpcCommandMessage>(
            message =>
            {
                message.Name = model.Name;
                message.Address = model.Address;
            })), true);
    }

    /// <summary>
    ///     Update a department
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("{id}"), Authorize]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<IActionResult> UpdateDepartment(string id, [FromBody] UpdateDepartmentModel model)
    {
        return this.TryAsync(() => this._departmentsGrpcService.UpdateDepartment(this.CreateCommandMessage<UpdateDepartmentGrpcCommandMessage>(
            message =>
            {
                message.Id = id;
                message.Name = model.Name;
                message.Address = model.Address;
            })), true);
    }

    /// <summary>
    ///     Delete a department
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("{id}"), Authorize]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<IActionResult> DeleteDepartmentById(string id)
    {
        return this.TryAsync(() =>
            this._departmentsGrpcService.DeleteDepartmentById(CreateCommandMessage<DeleteDepartmentByIdGrpcCommandMessage>(message => message.Id = id)), true);
    }
}