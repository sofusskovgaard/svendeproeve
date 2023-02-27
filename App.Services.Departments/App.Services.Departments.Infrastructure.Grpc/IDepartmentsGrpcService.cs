using App.Services.Departments.Infrastructure.Grpc.CommandMessages;
using App.Services.Departments.Infrastructure.Grpc.CommandResults;
using ProtoBuf.Grpc.Configuration;

namespace App.Services.Departments.Infrastructure.Grpc
{
    [Service("app.services.departments")]
    public interface IDepartmentsGrpcService
    {
        [Operation]
        ValueTask<CreateDepartmentCommandResult> CreateDepartment(CreateDepartmentCommandMessage message);

        [Operation]
        ValueTask<GetAllDepartmentsCommandResult> GetAllDepartments(GetAllDepartmentsCommandMessage message);

        [Operation]
        ValueTask<GetDepartmentsByNameCommandResult> GetDepartmentsByName(GetDepartmentsByNameCommandMessage message);

        [Operation]
        ValueTask<GetDepartmentsByOrganizationIdCommandResult> GetDepartmentsByOrganizationId(GetDepartmentsByOrganizationIdCommandMessage message);

        [Operation]
        ValueTask<GetDepartmentByIdCommandResult> GetDepartmentById(GetDepartmentByIdCommandMessage message);

        [Operation]
        ValueTask<DeleteDepartmentByIdCommandResult> DeleteDepartmentById(DeleteDepartmentByIdCommandMessage message);

        [Operation]
        ValueTask<UpdateDepartmentCommandResult> UpdateDepartment(UpdateDepartmentCommandMessage message);
    }
}