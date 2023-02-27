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
    }
}