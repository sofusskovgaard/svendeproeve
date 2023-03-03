using App.Services.Departments.Infrastructure.Grpc.CommandMessages;
using App.Services.Departments.Infrastructure.Grpc.CommandResults;
using ProtoBuf.Grpc.Configuration;

namespace App.Services.Departments.Infrastructure.Grpc
{
    [Service("app.services.departments")]
    public interface IDepartmentsGrpcService
    {
        [Operation]
        ValueTask<CreateDepartmentCommandResult> CreateDepartment(CreateDepartmentGrpcCommandMessage message);

        [Operation]
        ValueTask<GetAllDepartmentsCommandResult> GetAllDepartments(GetAllDepartmentsGrpcCommandMessage message);

        [Operation]
        ValueTask<GetDepartmentsByNameCommandResult> GetDepartmentsByName(GetDepartmentsByNameGrpcCommandMessage message);

        [Operation]
        ValueTask<GetDepartmentsByOrganizationIdCommandResult> GetDepartmentsByOrganizationId(GetDepartmentsByOrganizationIdGrpcCommandMessage message);

        [Operation]
        ValueTask<GetDepartmentByIdCommandResult> GetDepartmentById(GetDepartmentByIdGrpcCommandMessage message);

        [Operation]
        ValueTask<DeleteDepartmentByIdCommandResult> DeleteDepartmentById(DeleteDepartmentByIdGrpcCommandMessage message);

        [Operation]
        ValueTask<UpdateDepartmentCommandResult> UpdateDepartment(UpdateDepartmentGrpcCommandMessage message);
    }
}