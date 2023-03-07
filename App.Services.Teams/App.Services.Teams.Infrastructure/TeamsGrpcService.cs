using App.Common.Grpc;
using App.Data.Services;
using App.Infrastructure.Grpc;
using App.Services.Teams.Common.Dtos;
using App.Services.Teams.Data.Entities;
using App.Services.Teams.Infrastructure.Commands;
using App.Services.Teams.Infrastructure.Grpc;
using App.Services.Teams.Infrastructure.Grpc.CommandMessages;
using App.Services.Teams.Infrastructure.Grpc.CommandResults;
using AutoMapper;
using MassTransit;
using MongoDB.Driver;

namespace App.Services.Teams.Infrastructure;

public class TeamsGrpcService : BaseGrpcService, ITeamsGrpcService
{
    private readonly IEntityDataService _entityDataService;

    private readonly IMapper _mapper;

    private readonly IPublishEndpoint _publishEndpoint;

    public TeamsGrpcService(IEntityDataService entityDataService, IMapper mapper, IPublishEndpoint publishEndpoint)
    {
        _entityDataService = entityDataService;
        _mapper = mapper;
        _publishEndpoint = publishEndpoint;
    }

    public ValueTask<GetAllTeamsGrpcCommandResult> GetAllTeams(GetAllTeamsGrpcCommandMessage message)
    {
        return TryAsync(async () =>
        {
            var filters = new List<FilterDefinition<TeamEntity>>();

            if (!string.IsNullOrEmpty(message.SearchText))
            {
                filters.Add(new FilterDefinitionBuilder<TeamEntity>().Text(message.SearchText));
            }

            if (!string.IsNullOrEmpty(message.GameId))
            {
                filters.Add(new FilterDefinitionBuilder<TeamEntity>().Eq(entity => entity.GameId, message.GameId));
            }

            if (!string.IsNullOrEmpty(message.OrganizationId))
            {
                filters.Add(new FilterDefinitionBuilder<TeamEntity>().Eq(entity => entity.OrganizationId, message.OrganizationId));
            }

            if (!string.IsNullOrEmpty(message.MemberId))
            {
                filters.Add(new FilterDefinitionBuilder<TeamEntity>().AnyEq(entity => entity.MembersId, message.MemberId));
            }

            if (!string.IsNullOrEmpty(message.ManagerId))
            {
                filters.Add(new FilterDefinitionBuilder<TeamEntity>().Eq(entity => entity.ManagerId, message.ManagerId));
            }

            var entities = await _entityDataService.ListEntities<TeamEntity>(filter => filters.Any() ? filter.And(filters) : FilterDefinition<TeamEntity>.Empty);

            return new GetAllTeamsGrpcCommandResult
            {
                Metadata = new GrpcCommandResultMetadata{ Success = true },
                Data = _mapper.Map<IEnumerable<TeamDto>>(entities)
            };
        });
    }

    public ValueTask<GetTeamByIdGrpcCommandResult> GetTeamById(GetTeamByIdGrpcCommandMessage message)
    {
        return TryAsync(async () =>
        {
            var entity = await _entityDataService.GetEntity<TeamEntity>(message.Id);

            return new GetTeamByIdGrpcCommandResult
            {
                Metadata = new GrpcCommandResultMetadata{ Success = true },
                Data = _mapper.Map<TeamDto>(entity)
            };
        });
    }

    public ValueTask<CreateTeamGrpcCommandResult> CreateTeam(CreateTeamGrpcCommandMessage message)
    {
        return TryAsync(async () =>
        {
            await _publishEndpoint.Publish(new CreateTeamCommandMessage
            {
                Name = message.Name,
                Bio = message.Bio,
                ProfilePicturePath = message.ProfilePicturePath,
                CoverPicturePath = message.CoverPicturePath,
                GameId = message.GameId,
                OrganizationId = message.OrganizationId,
                MembersId = message.MembersId,
                ManagerId = message.ManagerId
            });

            return new CreateTeamGrpcCommandResult{ Metadata = new GrpcCommandResultMetadata{ Success = true } };
        });
    }

    public ValueTask<DeleteTeamByIdGrpcCommandResult> DeleteTeamById(DeleteTeamByIdGrpcCommandMessage message)
    {
        return TryAsync(async () =>
        {
            await _publishEndpoint.Publish(new DeleteTeamCommandMessage{ Id = message.Id });
            return new DeleteTeamByIdGrpcCommandResult{ Metadata = new GrpcCommandResultMetadata{ Success = true } };
        });
    }

    public ValueTask<UpdateTeamGrpcCommandResult> UpdateTeam(UpdateTeamGrpcCommandMessage message)
    {
        return TryAsync(async () =>
        {
            await _publishEndpoint.Publish(new UpdateTeamCommandMessage
            {
                TeamId = message.TeamId,
                Name = message.TeamDto.Name,
                Bio = message.TeamDto.Bio,
                ProfilePicturePath = message.TeamDto.ProfilePicturePath,
                CoverPicturePath = message.TeamDto.CoverPicturePath
            });

            return new UpdateTeamGrpcCommandResult{ Metadata = new GrpcCommandResultMetadata{ Success = true } };
        });
    }
}