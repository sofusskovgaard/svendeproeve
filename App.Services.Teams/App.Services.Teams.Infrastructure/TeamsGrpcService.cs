using App.Data.Services;
using App.Infrastructure.Grpc;
using App.Services.Teams.Common.Dtos;
using App.Services.Teams.Data.Entities;
using App.Services.Teams.Infrastructure.Events;
using App.Services.Teams.Infrastructure.Grpc;
using App.Services.Teams.Infrastructure.Grpc.CommandMessages;
using App.Services.Teams.Infrastructure.Grpc.CommandResults;
using AutoMapper;
using Grpc.Core;
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

    public ValueTask<GetAllTeamsCommandResult> GetAllTeams(GetAllTeamsCommandMessage message)
    {
        return TryAsync(async () =>
        {
            var teams = await _entityDataService.ListEntities<TeamEntity>();

            return new GetAllTeamsCommandResult()
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

    public ValueTask<GetTeamsByOrganizationIdCommandResult> GetTeamsByOrganizationId(GetTeamsByOrganizationIdCommandMessage message)
    {
        return TryAsync(async () =>
        {
            var teams = await _entityDataService.ListEntities(new ExpressionFilterDefinition<TeamEntity>(entity => entity.OrganizationId == message.OrganizationId));
            return new GetTeamsByOrganizationIdCommandResult()
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

    public ValueTask<GetTeamsByMemberIdCommandResult> GetTeamsByMemberId(GetTeamsByMemberIdCommandMessage message)
    {
        return TryAsync(async () =>
        {
            var teams = await _entityDataService.ListEntities(new ExpressionFilterDefinition<TeamEntity>(entity => entity.MembersId.Contains(message.MemberId)));

            return new GetTeamsByMemberIdCommandResult()
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

    public ValueTask<GetTeamsByNameCommandResult> GetTeamsByName(GetTeamsByNameCommandMessage message)
    {
        return TryAsync(async () =>
        {
            var teams = await _entityDataService.ListEntities(new ExpressionFilterDefinition<TeamEntity>(entity => entity.Name.Contains(message.Name)));

            return new GetTeamsByNameCommandResult()
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

    public ValueTask<GetTeamsByGameIdCommandResult> GetTeamsByGameId(GetTeamsByGameIdCommandMessage message)
    {
        return TryAsync(async () =>
        {
            var teams = await _entityDataService.ListEntities(new ExpressionFilterDefinition<TeamEntity>(entity => entity.GameId == message.GameId));

            return new GetTeamsByGameIdCommandResult()
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

    public ValueTask<GetTeamsByManagerIdCommandResult> GetTeamsByManagerId(GetTeamsByManagerIdCommandMessage message)
    {
        return TryAsync(async () =>
        {
            var teams = await _entityDataService.ListEntities(new ExpressionFilterDefinition<TeamEntity>(entity => entity.ManagerId == message.ManagerId));

            return new GetTeamsByManagerIdCommandResult()
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

    public ValueTask<GetTeamByIdCommandResult> GetTeamById(GetTeamByIdCommandMessage message)
    {
        return TryAsync(async () =>
        {
            var team = await _entityDataService.GetEntity<TeamEntity>(message.Id);

            return new GetTeamByIdCommandResult()
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

    public ValueTask<CreateTeamCommandResult> CreateTeam(CreateTeamCommandMessage message)
    {
        return TryAsync(async () =>
        {
            TeamEntity team = new TeamEntity()
            {
                Name = message.Name,
                Bio = message.Bio,
                ProfilePicturePath = message.ProfilePicturePath,
                CoverPicturePath = message.CoverPicturePath,
                GameId = message.GameId,
                OrganizationId = message.OrganizationId,
                MembersId = message.MembersId,
                ManagerId = message.ManagerId,
            };

            await _entityDataService.Create<TeamEntity>(team);

            return new CreateTeamCommandResult()
            {
                Metadata = new GrpcCommandResultMetadata()
                {
                    Success = true,
                    Message = "Team oprettet"
                }
            };
        });
    }

    public ValueTask<DeleteTeamByIdCommandResult> DeleteTeamById(DeleteTeamByIdCommandMessage message)
    {
        return TryAsync(async () =>
        {
            TeamEntity team = await _entityDataService.GetEntity<TeamEntity>(message.Id);

            GrpcCommandResultMetadata metadata;

            if (team != null)
            {
                //await _entityDataService.Delete<TeamEntity>(team);

                metadata = new GrpcCommandResultMetadata()
                {
                    Success = true,
                    Message = "Team deleted"
                };

                await _publishEndpoint.Publish(new TeamDeletedEventMessage() { Id = message.Id });
            }
            else
            {
                metadata = new GrpcCommandResultMetadata()
                {
                    Success = false,
                    Message = "Could not find any teams with that id"
                };
            }

            return new DeleteTeamByIdCommandResult()
            {
                Metadata = metadata
            };
        });
    }

    public ValueTask<UpdateTeamCommandResult> UpdateTeam(UpdateTeamCommandMessage message)
    {
        return TryAsync(async () =>
        {
            TeamEntity team = _mapper.Map<TeamEntity>(message.TeamDto);
            await _entityDataService.Update<TeamEntity>(team);

            return new UpdateTeamCommandResult()
            {
                Metadata = new GrpcCommandResultMetadata()
                {
                    Success = true,
                    Message = "Team updated"
                }
            };
        });
    }
}
