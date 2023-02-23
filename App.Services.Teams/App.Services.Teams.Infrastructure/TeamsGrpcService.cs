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

    public async Task<GetTeamsByOrganizationIdCommandResult> GetTeamsByOrganizationId(GetTeamsByOrganizationIdCommandMessage message)
    {
        return new GetTeamsByOrganizationIdCommandResult()
        {
            Metadata = new GrpcCommandResultMetadata()
            {
                Success = true,
                Message = "Will this work?"
            },
            TeamsEnties = (await _entityDataService.ListEntities<TeamEntity>()).Where(to => to.OrganizationId == message.Id)
        };
    }

    public async Task<GetTeamsByNameCommandResult> GetTeamsByName(GetTeamsByNameCommandMessage message)
    {
        return new GetTeamsByNameCommandResult()
        {
            Metadata = new GrpcCommandResultMetadata()
            {
                Success = true,
                Message = "Getting Teams"
            },
            TeamsEnties = (await _entityDataService.ListEntities<TeamEntity>()).Where(t => t.Name.Contains(message.Name))
        };
    }

    public async Task<GetTeamByIdCommandResult> GetTeamById(GetTeamByIdCommandMessage message)
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

    public async Task<CreateTeamCommandResult> CreateTeam(CreateTeamCommandMessage message)
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
