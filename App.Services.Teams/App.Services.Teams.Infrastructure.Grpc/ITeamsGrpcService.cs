using App.Services.Teams.Infrastructure.Grpc.CommandMessages;
using App.Services.Teams.Infrastructure.Grpc.CommandResults;
using ProtoBuf.Grpc.Configuration;

namespace App.Services.Teams.Infrastructure.Grpc
{
    [Service("app.services.teams")]
    public interface ITeamsGrpcService
    {
        [Operation]
        Task<GetTeamsByOrganizationIdCommandResult> GetTeamsByOrganizationId(GetTeamsByOrganizationIdCommandMessage message);

        [Operation]
        Task<GetTeamsByNameCommandResult> GetTeamsByName(GetTeamsByNameCommandMessage message);

        [Operation]
        Task<GetTeamsByMemberIdCommandResult> GetTeamsByMemberId(GetTeamsByMemberIdCommandMessage message);

        [Operation]
        Task<GetTeamsByGameIdCommandResult> GetTeamsByGameId(GetTeamsByGameIdCommandMessage message);

        [Operation]
        Task<GetTeamsByManagerIdCommandResult> GetTeamsByManagerId(GetTeamsByManagerIdCommandMessage message);

        [Operation]
        Task<GetTeamByIdCommandResult> GetTeamById(GetTeamByIdCommandMessage message);

        [Operation]
        Task<CreateTeamCommandResult> CreateTeam(CreateTeamCommandMessage message);
    }
}