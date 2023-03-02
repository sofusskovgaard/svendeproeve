using App.Data.Services;
using App.Infrastructure.Grpc;
using App.Services.Organizations.Common.Dtos;
using App.Services.Organizations.Data.Entities;
using App.Services.Organizations.Infrastructure.Grpc;
using App.Services.Organizations.Infrastructure.Grpc.CommandMessages;
using App.Services.Organizations.Infrastructure.Grpc.CommandResults;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using ProtoBuf.Grpc.Configuration;

namespace App.Services.Organizations.Infrastructure
{
    public class OrganizationsGrpcService : IOrganizationsGrpcService
    {
        private readonly IEntityDataService _entityDataservice;

        private readonly IMapper _mapper;
        public OrganizationsGrpcService(IEntityDataService entityDataService, IMapper mapper)
        {
            this._entityDataservice = entityDataService;
            _mapper = mapper;
        }
        public async ValueTask<GetOrganizationByIdCommandResult> GetOrganizationById(GetOrganizationByIdCommandMessage message)
        {
            var entity = await this._entityDataservice.GetEntity<OrganizationEntity>(message.Id);
            return new GetOrganizationByIdCommandResult()
            {
                Metadata = new GrpcCommandResultMetadata()
                {
                    Success = true,
                    Message = "Oh my lord it clearly worked"
                },
                Organization = _mapper.Map<OrganizationDto>(entity),

            };
        }
        public async ValueTask<GetOrganizationsByNameCommandResult> GetOrganizationsByName(GetOrganizationsByNameCommandMessage message)
        {
            var res = (await this._entityDataservice.ListEntities<OrganizationEntity>()).Where(x => x.Name.Contains(message.Name)); //TODO: better Where statement
            return new GetOrganizationsByNameCommandResult()
            {
                Metadata = new GrpcCommandResultMetadata()
                {
                    Success = true,
                    Message = "completed"
                },
                Organizations = res.ToList()
            };
        }

        public async ValueTask<GetOrganizationsByAddressCommandResult> GetOrganizationsByAddress(GetOrganizationsByAddressCommandMessage message)
        {
            var res = (await this._entityDataservice.ListEntities<OrganizationEntity>()).Where(x => x.Address.Contains(message.Address)); //TODO: better Where statement
            return new GetOrganizationsByAddressCommandResult()
            {
                Metadata = new GrpcCommandResultMetadata()
                {
                    Success = true,
                    Message = "completed"
                },
                Organizations = res.ToList()
            };
        }
        public async Task<CreateOrganizationCommandResult> CreateOrganization(CreateOrganizationCommandMessage message)
        {
            //TODO: masstransit
            var res = await this._entityDataservice.Create<OrganizationEntity>(new OrganizationEntity { Name = message.Name, DepartmentId = message.DepartmentId });
            return new CreateOrganizationCommandResult()
            {
                Metadata = new GrpcCommandResultMetadata()
                {
                    Success = true,
                    Message = "completed"
                },
                OrganizationId = res.Id
            };
        }
    }
}