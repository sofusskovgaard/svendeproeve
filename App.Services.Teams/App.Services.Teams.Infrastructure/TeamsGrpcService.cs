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
using MongoDB.Bson;
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

            team = await _entityDataService.Create<TeamEntity>(team);

            TeamCreatedEventMessage eventMessage = new TeamCreatedEventMessage()
            {
                Id = team.Id,
                OrganizationId = team.OrganizationId,
                UsersId = team.MembersId
            };

            if (!team.MembersId.Contains(team.ManagerId) && !string.IsNullOrEmpty(team.ManagerId))
            {
                eventMessage.UsersId.Append(team.ManagerId);
            }

            await _publishEndpoint.Publish(eventMessage);

            return new CreateTeamGrpcCommandResult()
            {
                Metadata = new GrpcCommandResultMetadata()
                {
                    Success = true,
                    Message = "Team oprettet"
                }
            };
        });
    }

    public ValueTask<DeleteTeamByIdGrpcCommandResult> DeleteTeamById(DeleteTeamByIdGrpcCommandMessage message)
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

            return new DeleteTeamByIdGrpcCommandResult()
            {
                Metadata = metadata
            };
        });
    }

    public ValueTask<UpdateTeamGrpcCommandResult> UpdateTeam(UpdateTeamGrpcCommandMessage message)
    {
        return TryAsync(async () =>
        {
            //TeamEntity team = _mapper.Map<TeamEntity>(message.TeamDto);
            //await _entityDataService.Update<TeamEntity>(team);

            var team = await _entityDataService.GetEntity<TeamEntity>(message.TeamId);


            var updateDefinition = new UpdateDefinitionBuilder<TeamEntity>().Set(entity => entity.Name, message.TeamDto.Name);

            if (message.TeamDto.Bio != team.Bio)
            {
                updateDefinition.Set(entity => entity.Bio, message.TeamDto.Bio);
            }
            if (message.TeamDto.ProfilePicturePath != team.ProfilePicturePath)
            {
                updateDefinition.Set(entity => entity.ProfilePicturePath, message.TeamDto.ProfilePicturePath);
            }
            if (message.TeamDto.CoverPicturePath != team.CoverPicturePath)
            {
                updateDefinition.Set(entity => entity.CoverPicturePath, message.TeamDto.CoverPicturePath);
            }

            var result = await _entityDataService.Update<TeamEntity>(filter => filter.Eq(entity => entity.Id, team.Id), _ => updateDefinition);
            
            return new UpdateTeamGrpcCommandResult()
            {
                Metadata = new GrpcCommandResultMetadata()
                {
                    Success = result
                }
            };
        });
    }
}
