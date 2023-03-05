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
            var teams = await _entityDataService.ListEntities<TeamEntity>();

            return new GetAllTeamsGrpcCommandResult()
            {
                Metadata = new GrpcCommandResultMetadata()
                {
                    Success = true,
                    Message = "Getting teams"
                },
                TeamDtos = _mapper.Map<IEnumerable<TeamDto>>(teams)
            };
        });
    }

    public ValueTask<GetTeamsByOrganizationIdGrpcCommandResult> GetTeamsByOrganizationId(GetTeamsByOrganizationIdGrpcCommandMessage message)
    {
        return TryAsync(async () =>
        {
            var teams = await _entityDataService.ListEntities(new ExpressionFilterDefinition<TeamEntity>(entity => entity.OrganizationId == message.OrganizationId));
            return new GetTeamsByOrganizationIdGrpcCommandResult()
            {
                Metadata = new GrpcCommandResultMetadata()
                {
                    Success = true,
                    Message = "Getting teams"
                },
                TeamDtos = _mapper.Map<IEnumerable<TeamDto>>(teams)
            };
        });
    }

    public ValueTask<GetTeamsByMemberIdGrpcCommandResult> GetTeamsByMemberId(GetTeamsByMemberIdGrpcCommandMessage message)
    {
        return TryAsync(async () =>
        {
            var teams = await _entityDataService.ListEntities(new ExpressionFilterDefinition<TeamEntity>(entity => entity.MembersId.Contains(message.MemberId)));

            return new GetTeamsByMemberIdGrpcCommandResult()
            {
                Metadata = new GrpcCommandResultMetadata()
                {
                    Success = true,
                    Message = "Getting teams"
                },
                TeamDtos = _mapper.Map<IEnumerable<TeamDto>>(teams)
            };
        });
    }

    public ValueTask<GetTeamsByNameGrpcCommandResult> GetTeamsByName(GetTeamsByNameGrpcCommandMessage message)
    {
        return TryAsync(async () =>
        {
            var teams = await _entityDataService.ListEntities(new ExpressionFilterDefinition<TeamEntity>(entity => entity.Name.Contains(message.Name)));

            return new GetTeamsByNameGrpcCommandResult()
            {
                Metadata = new GrpcCommandResultMetadata()
                {
                    Success = true,
                    Message = "Getting teams"
                },
                TeamDtos = _mapper.Map<IEnumerable<TeamDto>>(teams)
            };
        });
    }

    public ValueTask<GetTeamsByGameIdGrpcCommandResult> GetTeamsByGameId(GetTeamsByGameIdGrpcCommandMessage message)
    {
        return TryAsync(async () =>
        {
            var teams = await _entityDataService.ListEntities(new ExpressionFilterDefinition<TeamEntity>(entity => entity.GameId == message.GameId));

            return new GetTeamsByGameIdGrpcCommandResult()
            {
                Metadata = new GrpcCommandResultMetadata()
                {
                    Success = true,
                    Message = "Getting teams"
                },
                TeamDtos = _mapper.Map<IEnumerable<TeamDto>>(teams)
            };
        });
    }

    public ValueTask<GetTeamsByManagerIdGrpcCommandResult> GetTeamsByManagerId(GetTeamsByManagerIdGrpcCommandMessage message)
    {
        return TryAsync(async () =>
        {
            var teams = await _entityDataService.ListEntities(new ExpressionFilterDefinition<TeamEntity>(entity => entity.ManagerId == message.ManagerId));

            return new GetTeamsByManagerIdGrpcCommandResult()
            {
                Metadata = new GrpcCommandResultMetadata()
                {
                    Success = true,
                    Message = "Getting teams"
                },
                TeamDtos = _mapper.Map<IEnumerable<TeamDto>>(teams)
            };
        });
    }

    public ValueTask<GetTeamByIdGrpcCommandResult> GetTeamById(GetTeamByIdGrpcCommandMessage message)
    {
        return TryAsync(async () =>
        {
            var team = await _entityDataService.GetEntity<TeamEntity>(message.Id);

            return new GetTeamByIdGrpcCommandResult()
            {
                Metadata = new GrpcCommandResultMetadata()
                {
                    Success = true,
                    Message = "Returning team"
                },
                TeamDto = _mapper.Map<TeamDto>(team)
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
                ManagerId = message.ManagerId,
            });

            return new CreateTeamGrpcCommandResult()
            {
                Metadata = new GrpcCommandResultMetadata()
                {
                    Success = true
                }
            };
        });
    }

    public ValueTask<DeleteTeamByIdGrpcCommandResult> DeleteTeamById(DeleteTeamByIdGrpcCommandMessage message)
    {
        return TryAsync(async () =>
        {
            await _publishEndpoint.Publish(new DeleteTeamCommandMessage
            {
                Id = message.Id
            });

            return new DeleteTeamByIdGrpcCommandResult()
            {
                Metadata = new GrpcCommandResultMetadata()
                {
                    Success = true,
                }
            };
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

            return new UpdateTeamGrpcCommandResult()
            {
                Metadata = new GrpcCommandResultMetadata()
                {
                    Success = true
                }
            };
        });
    }
}
