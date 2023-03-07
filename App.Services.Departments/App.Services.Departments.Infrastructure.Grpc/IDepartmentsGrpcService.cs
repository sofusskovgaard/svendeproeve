using App.Services.Departments.Infrastructure.Grpc.CommandMessages;
using App.Services.Departments.Infrastructure.Grpc.CommandResults;
using ProtoBuf.Grpc.Configuration;

namespace App.Services.Departments.Infrastructure.Grpc;

[Service("app.services.departments")]
public interface IDepartmentsGrpcService
{
    [Operation]
    ValueTask<CreateDepartmentGrpcCommandResult> CreateDepartment(CreateDepartmentGrpcCommandMessage message);

    [Operation]
    ValueTask<GetAllDepartmentsGrpcCommandResult> GetAllDepartments(GetAllDepartmentsGrpcCommandMessage message);

    [Operation]
    ValueTask<GetDepartmentsByNameGrpcCommandResult> GetDepartmentsByName(GetDepartmentsByNameGrpcCommandMessage message);

    [Operation]
    ValueTask<GetDepartmentByIdGrpcCommandResult> GetDepartmentById(GetDepartmentByIdGrpcCommandMessage message);

    [Operation]
    ValueTask<DeleteDepartmentByIdGrpcCommandResult> DeleteDepartmentById(DeleteDepartmentByIdGrpcCommandMessage message);

    [Operation]
    ValueTask<UpdateDepartmentGrpcCommandResult> UpdateDepartment(UpdateDepartmentGrpcCommandMessage message);
}