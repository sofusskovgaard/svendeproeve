using App.Data.Services;
using App.Infrastructure.Grpc;
using App.Services.Teams.Data.Entities;
using App.Services.Teams.Infrastructure.Grpc;
using App.Services.Teams.Infrastructure.Grpc.CommandMessages;
using App.Services.Teams.Infrastructure.Grpc.CommandResults;
using Grpc.Core;

namespace App.Services.Teams.Infrastructure;

public class TeamsGrpcService : BaseGrpcService, ITeamsGrpcService
{
    private readonly IEntityDataService _entityDataService;
    public TeamsGrpcService(IEntityDataService entityDataService)
    {
        _entityDataService = entityDataService;
    }

    public ValueTask<GetAllTeamsCommandResult> GetAllTeams(GetAllTeamsCommandMessage message)
    {
        return TryAsync(async () =>
        {
            return new GetAllTeamsCommandResult()
            {
                Metadata = new GrpcCommandResultMetadata()
                {
                    Success = true,
                    Message = "Getting teams"
                },
                TeamsEnties = await _entityDataService.ListEntities<TeamEntity>()
            };
        });
    }

    public ValueTask<GetTeamsByOrganizationIdCommandResult> GetTeamsByOrganizationId(GetTeamsByOrganizationIdCommandMessage message)
    {
        return TryAsync(async () =>
        {
            return new GetTeamsByOrganizationIdCommandResult()
            {
                Metadata = new GrpcCommandResultMetadata()
                {
                    Success = true,
                    Message = "Getting teams"
                },
                TeamsEnties = (await _entityDataService.ListEntities<TeamEntity>()).Where(to => to.OrganizationId == message.OrganizationId)
            };
        });
    }

    public ValueTask<GetTeamsByMemberIdCommandResult> GetTeamsByMemberId(GetTeamsByMemberIdCommandMessage message)
    {
        return TryAsync(async () =>
        {
            return new GetTeamsByMemberIdCommandResult()
            {
                Metadata = new GrpcCommandResultMetadata()
                {
                    Success = true,
                    Message = "Getting teams"
                },
                TeamsEnties = (await _entityDataService.ListEntities<TeamEntity>()).Where(tm => tm.MembersId.Contains(message.MemberId))
            };
        });
    }

    public ValueTask<GetTeamsByNameCommandResult> GetTeamsByName(GetTeamsByNameCommandMessage message)
    {
        return TryAsync(async () =>
        {
            return new GetTeamsByNameCommandResult()
            {
                Metadata = new GrpcCommandResultMetadata()
                {
                    Success = true,
                    Message = "Getting teams"
                },
                TeamsEnties = (await _entityDataService.ListEntities<TeamEntity>()).Where(t => t.Name.Contains(message.Name))
            };
        });
    }

    public ValueTask<GetTeamsByGameIdCommandResult> GetTeamsByGameId(GetTeamsByGameIdCommandMessage message)
    {
        return TryAsync(async () =>
        {
            return new GetTeamsByGameIdCommandResult()
            {
                Metadata = new GrpcCommandResultMetadata()
                {
                    Success = true,
                    Message = "Getting teams"
                },
                TeamsEnties = (await _entityDataService.ListEntities<TeamEntity>()).Where(tg => tg.GameId == message.GameId)
            };
        });
    }

    public ValueTask<GetTeamsByManagerIdCommandResult> GetTeamsByManagerId(GetTeamsByManagerIdCommandMessage message)
    {
        return TryAsync(async () =>
        {
            return new GetTeamsByManagerIdCommandResult()
            {
                Metadata = new GrpcCommandResultMetadata()
                {
                    Success = true,
                    Message = "Getting teams"
                },
                TeamsEnties = (await _entityDataService.ListEntities<TeamEntity>()).Where(tm => tm.ManagerId == message.ManagerId)
            };
        });
    }

    public ValueTask<GetTeamByIdCommandResult> GetTeamById(GetTeamByIdCommandMessage message)
    {
        return TryAsync(async () =>
        {
            return new GetTeamByIdCommandResult()
            {
                Metadata = new GrpcCommandResultMetadata()
                {
                    Success = true,
                    Message = "Returning team"
                },
                TeamEntity = await _entityDataService.GetEntity<TeamEntity>(message.Id)
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
                await _entityDataService.Delete<TeamEntity>(team);

                metadata = new GrpcCommandResultMetadata()
                {
                    Success = true,
                    Message = "Team deleted"
                };
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
}
