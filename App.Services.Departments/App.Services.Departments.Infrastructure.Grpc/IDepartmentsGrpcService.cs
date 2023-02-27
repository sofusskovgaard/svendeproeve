using App.Services.Departments.Infrastructure.Grpc.CommandMessages;
using App.Services.Departments.Infrastructure.Grpc.CommandResults;
using ProtoBuf.Grpc.Configuration;

namespace App.Services.Departments.Infrastructure.Grpc
{
    [Service]
    public interface IServiceNameGrpcService
    {
        [Operation]
        ValueTask<CreateDepartmentCommandResult> CreateDepartment(CreateDepartmentCommandMessage message);
    }
}