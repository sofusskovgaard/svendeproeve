using App.Services.Teams.Infrastructure.Grpc.CommandMessages;
using App.Services.Teams.Infrastructure.Grpc.CommandResults;
using ProtoBuf.Grpc.Configuration;

namespace App.Services.Teams.Infrastructure.Grpc
{
    [Service("app.services.teams")]
    public interface ITeamsGrpcService
    {
        [Operation]
        ValueTask<GetAllTeamsCommandResult> GetAllTeams(GetAllTeamsCommandMessage message);

        [Operation]
        ValueTask<GetTeamsByOrganizationIdCommandResult> GetTeamsByOrganizationId(GetTeamsByOrganizationIdCommandMessage message);

        [Operation]
        ValueTask<GetTeamsByNameCommandResult> GetTeamsByName(GetTeamsByNameCommandMessage message);

        [Operation]
        ValueTask<GetTeamsByMemberIdCommandResult> GetTeamsByMemberId(GetTeamsByMemberIdCommandMessage message);

        [Operation]
        ValueTask<GetTeamsByGameIdCommandResult> GetTeamsByGameId(GetTeamsByGameIdCommandMessage message);

        [Operation]
        ValueTask<GetTeamsByManagerIdCommandResult> GetTeamsByManagerId(GetTeamsByManagerIdCommandMessage message);

        [Operation]
        ValueTask<GetTeamByIdCommandResult> GetTeamById(GetTeamByIdCommandMessage message);

        [Operation]
        ValueTask<CreateTeamCommandResult> CreateTeam(CreateTeamCommandMessage message);
    }
}