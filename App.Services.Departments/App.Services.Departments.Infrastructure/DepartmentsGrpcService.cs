using App.Data.Services;
using App.Infrastructure.Grpc;
using App.Services.Departments.Data.Entities;
using App.Services.Departments.Infrastructure.Grpc;
using App.Services.Departments.Infrastructure.Grpc.CommandMessages;
using App.Services.Departments.Infrastructure.Grpc.CommandResults;
using AutoMapper;
using ProtoBuf.Grpc.Configuration;

namespace App.Services.Departments.Infrastructure
{
    public class DepartmentsGrpcService : BaseGrpcService, IDepartmentsGrpcService
    {
        private readonly IEntityDataService _entityDataService;
        private readonly IMapper _mapper;
        public DepartmentsGrpcService(IEntityDataService entityDataService, IMapper mapper)
        {
            _entityDataService = entityDataService;
            _mapper = mapper;
        }

        public ValueTask<CreateDepartmentCommandResult> CreateDepartment(CreateDepartmentCommandMessage message)
        {
            return TryAsync(async () =>
            {
                DepartmentEntity department = new DepartmentEntity()
                {
                    Name = message.Name,
                    Address = message.Address,
                    OrganizationIds = message.OrganizationIds
                };

                await _entityDataService.Create<DepartmentEntity>(department);

                return new CreateDepartmentCommandResult()
                {
                    Metadata = new GrpcCommandResultMetadata
                    {
                        Success = true,
                        Message = "Department created"
                    }
                };
            });
        }
    }
}