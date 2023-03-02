using App.Data.Services;
using App.Infrastructure.Grpc;
using App.Services.Organizations.Common.Dtos;
using App.Services.Organizations.Data.Entities;
using App.Services.Organizations.Infrastructure.Commands;
using App.Services.Organizations.Infrastructure.Grpc;
using App.Services.Organizations.Infrastructure.Grpc.CommandMessages;
using App.Services.Organizations.Infrastructure.Grpc.CommandResults;
using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Http.HttpResults;
using ProtoBuf.Grpc.Configuration;

namespace App.Services.Organizations.Infrastructure
{
    public class OrganizationsGrpcService : BaseGrpcService, IOrganizationsGrpcService
    {
        private readonly IEntityDataService _entityDataservice;

        private readonly IPublishEndpoint _publishEndpoint;

        private readonly IMapper _mapper;
        public OrganizationsGrpcService(IEntityDataService entityDataService, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            this._entityDataservice = entityDataService;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }
        public ValueTask<GetOrganizationByIdGrpcCommandResult> GetOrganizationById(GetOrganizationByIdGrpcCommandMessage message)
        {
            return TryAsync(async () =>
            {
                var entity = await this._entityDataservice.GetEntity<OrganizationEntity>(message.Id);
                return new GetOrganizationByIdGrpcCommandResult()
                {
                    Metadata = new GrpcCommandResultMetadata()
                    {
                        Success = true,
                    },
                    Organization = _mapper.Map<OrganizationDto>(entity),

                };
            });
        }

        public ValueTask<GetOrganizationsGrpcCommandResult> GetOrganizations(GetOrganizationsGrpcCommandMessage message)
        {
            return TryAsync(async () =>
            {
                var res = await _entityDataservice.ListEntities<OrganizationEntity>();
                return new GetOrganizationsGrpcCommandResult
                {
                    Metadata = new GrpcCommandResultMetadata
                    {
                        Success = true
                    },
                    OrganizationDtos = _mapper.Map<OrganizationDto[]>(res)
                };
            });
        }
        public ValueTask<GetOrganizationsByNameGrpcCommandResult> GetOrganizationsByName(GetOrganizationsByNameGrpcCommandMessage message)
        {
            return TryAsync(async () =>
            {
                var res = (await this._entityDataservice.ListEntities<OrganizationEntity>()).Where(x => x.Name.Contains(message.Name)); //TODO: better Where statement
                return new GetOrganizationsByNameGrpcCommandResult()
                {
                    Metadata = new GrpcCommandResultMetadata()
                    {
                        Success = true,
                        Message = "completed"
                    },
                    Organizations = _mapper.Map<OrganizationDto[]>(res)
                };
            });
        }

        public ValueTask<GetOrganizationsByAddressGrpcCommandResult> GetOrganizationsByAddress(GetOrganizationsByAddressGrpcCommandMessage message)
        {
            return TryAsync(async () =>
            {
                var res = (await this._entityDataservice.ListEntities<OrganizationEntity>()).Where(x => x.Address.Contains(message.Address)); //TODO: better Where statement
                return new GetOrganizationsByAddressGrpcCommandResult()
                {
                    Metadata = new GrpcCommandResultMetadata()
                    {
                        Success = true,
                        Message = "completed"
                    },
                    Organizations = _mapper.Map<OrganizationDto[]>(res)
                };
            });
        }
        public ValueTask<CreateOrganizationGrpcCommandResult> CreateOrganization(CreateOrganizationGrpcCommandMessage message)
        {
            return TryAsync(async () =>
            {
                var createMessage = new CreateOrganizationCommandMessage
                {
                    Address = message.Address,
                    Bio = message.Bio,
                    CoverPicture = message.CoverPicture,
                    Name = message.Name,
                    ProfilePicture = message.ProfilePicture
                };

                await _publishEndpoint.Publish(createMessage);

                return new CreateOrganizationGrpcCommandResult
                {
                    Metadata = new GrpcCommandResultMetadata { Success = true }
                };
            });
        }

        public ValueTask<UpdateOrganizationGrpcCommandResult> UpdateOrganization(UpdateOrganizationGrpcCommandMessage message)
        {
            return TryAsync(async () =>
            {
                var updateMessage = new UpdateOrganizationCommandMessage
                {
                    Id = message.Id,
                    Address = message.Address,
                    Bio = message.Bio,
                    CoverPicture = message.CoverPicture,
                    Name = message.Name,
                    ProfilePicture = message.ProfilePicture
                };

                await _publishEndpoint.Publish(updateMessage);

                return new UpdateOrganizationGrpcCommandResult
                {
                    Metadata = new GrpcCommandResultMetadata { Success = true }
                };
            });
        }

        public ValueTask<DeleteOrganizationGrpcCommandResult> DeleteOrganization(DeleteOrganizationGrpcCommandMessage message)
        {
            return TryAsync(async () =>
            {
                var deleteMessage = new DeleteOrganizationCommandMessage
                {
                    Id = message.Id
                };

                await _publishEndpoint.Publish(deleteMessage);

                return new DeleteOrganizationGrpcCommandResult
                {
                    Metadata = new GrpcCommandResultMetadata { Success = true }
                };
            });

        }
    }
}