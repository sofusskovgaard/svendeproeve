using App.Data.Services;
using App.Infrastructure.Grpc;
using App.Services.Teams.Data.Entities;
using App.Services.Teams.Infrastructure.Grpc;
using App.Services.Teams.Infrastructure.Grpc.CommandMessages;
using App.Services.Teams.Infrastructure.Grpc.CommandResults;

namespace App.Services.Teams.Infrastructure;

public class TeamsGrpcService : ITeamsGrpcService
{
    private readonly IEntityDataService _entityDataService;
    public TeamsGrpcService(IEntityDataService entityDataService)
    {
        _entityDataService = entityDataService;
    }

    public async ValueTask<GetTeamsByOrganizationIdCommandResult> GetTeamsByOrganizationId(GetTeamsByOrganizationIdCommandMessage message)
    {
        return new GetTeamsByOrganizationIdCommandResult()
        {
            Metadata = new GrpcCommandResultMetadata()
            {
                Success = true,
                Message = "Will this work?"
            },
            TeamsEnties = (await _entityDataService.ListEntities<TeamEntity>()).Where(to => to.OrganizationId == message.OrganizationId)
        };
    }

    public async ValueTask<GetTeamsByMemberIdCommandResult> GetTeamsByMemberId(GetTeamsByMemberIdCommandMessage message)
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
    }

    public async ValueTask<GetTeamsByNameCommandResult> GetTeamsByName(GetTeamsByNameCommandMessage message)
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
    }

    public async ValueTask<GetTeamsByGameIdCommandResult> GetTeamsByGameId(GetTeamsByGameIdCommandMessage message)
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
    }

    public async ValueTask<GetTeamsByManagerIdCommandResult> GetTeamsByManagerId(GetTeamsByManagerIdCommandMessage message)
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
    }

    public async ValueTask<GetTeamByIdCommandResult> GetTeamById(GetTeamByIdCommandMessage message)
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
    }

    public async ValueTask<CreateTeamCommandResult> CreateTeam(CreateTeamCommandMessage message)
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
    }
}
