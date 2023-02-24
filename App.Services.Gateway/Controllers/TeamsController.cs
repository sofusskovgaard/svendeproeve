using App.Services.Gateway.Infrastructure;
using App.Services.Teams.Common.Dtos;
using App.Services.Teams.Infrastructure.Grpc;
using App.Services.Teams.Infrastructure.Grpc.CommandMessages;
using Microsoft.AspNetCore.Mvc;
using ProtoBuf;

namespace App.Services.Gateway.Controllers
{
    [Route("api/[controller]")]
    public class TeamsController : ApiController
    {
        private readonly ITeamsGrpcService _teamsGrpcService;

        public TeamsController(ITeamsGrpcService teamsGrpcService)
        {
            _teamsGrpcService = teamsGrpcService;
        }

        [HttpGet]
        [Route("Index")]
        public Task<IActionResult> Index()
        {
            return TryAsync(() => this._teamsGrpcService.GetAllTeams(new GetAllTeamsCommandMessage()));
        }

        [HttpGet]
        [Route("organizationteams")]
        public Task<IActionResult> GetTeamsByOrganizationId(string id)
        {
            return TryAsync(() => this._teamsGrpcService.GetTeamsByOrganizationId(new GetTeamsByOrganizationIdCommandMessage() { OrganizationId = id }));
        }

        [HttpGet]
        [Route("nameteams")]
        public Task<IActionResult> GetTeamsByName(string name)
        {
            return TryAsync(() => this._teamsGrpcService.GetTeamsByName(new GetTeamsByNameCommandMessage() { Name = name }));
        }

        [HttpGet]
        [Route("memberteams")]
        public Task<IActionResult> GetTeamsByMemberId(string memberId)
        {
            return TryAsync(() => this._teamsGrpcService.GetTeamsByMemberId(new GetTeamsByMemberIdCommandMessage() { MemberId = memberId }));
        }

        [HttpGet]
        [Route("gameteams")]
        public Task<IActionResult> GetTeamsByGameId(string gameId)
        {
            return TryAsync(() => this._teamsGrpcService.GetTeamsByGameId(new GetTeamsByGameIdCommandMessage() { GameId = gameId }));
        }

        [HttpGet]
        [Route("managerteams")]
        public Task<IActionResult> GetTeamsByManagerId(string managerId)
        {
            return TryAsync(() => this._teamsGrpcService.GetTeamsByManagerId(new GetTeamsByManagerIdCommandMessage() { ManagerId = managerId }));
        }

        [HttpGet]
        [Route("{id}")]
        public Task<IActionResult> GetTeamById(string id)
        {
            return TryAsync(() => this._teamsGrpcService.GetTeamById(new GetTeamByIdCommandMessage() { Id = id }));
        }

        [HttpPost]
        [Route("team")]
        public Task<IActionResult> CreateTeam([FromBody] CreateTeamModel model)
        {
            return TryAsync(() =>
            {
                var command = new CreateTeamCommandMessage
                {
                    Name = model.Name,
                    Bio = model.Bio,
                    ProfilePicturePath = model.ProfilePicturePath,
                    CoverPicturePath = model.CoverPicturePath,
                    GameId = model.GameId,
                    MembersId = model.MembersId,
                    ManagerId = model.ManagerId,
                    OrganizationId = model.OrganizationId
                };

                return _teamsGrpcService.CreateTeam(command);
            });
        }

        [HttpDelete]
        [Route("deleteteam")]
        public Task<IActionResult> DeleteTeamById(string id)
        {
            return TryAsync(() => this._teamsGrpcService.DeleteTeamById(new DeleteTeamByIdCommandMessage() { Id = id }));
        }

        [HttpPost]
        [Route("updateteam")]
        public Task<IActionResult> UpdateTeam([FromBody] UpdateTeamModel model)
        {
            return TryAsync(() =>
            {
                var command = new UpdateTeamCommandMessage
                {
                    TeamDto = new TeamDto
                    {
                        Id = model.Id,
                        Name = model.Name,
                        Bio = model.Bio,
                        ProfilePicturePath = model.ProfilePicturePath,
                        CoverPicturePath = model.CoverPicturePath,
                        GameId = model.GameId,
                        MembersId = model.MembersId,
                        ManagerId = model.ManagerId,
                        OrganizationId = model.OrganizationId
                    }
                };

                return _teamsGrpcService.UpdateTeam(command);
            });
        }
    }

    public record CreateTeamModel(string Name, string Bio, string ProfilePicturePath, string CoverPicturePath, string GameId, string[] MembersId, string ManagerId, string OrganizationId);
    public record UpdateTeamModel(string Id, string Name, string Bio, string ProfilePicturePath, string CoverPicturePath, string GameId, string[] MembersId, string ManagerId, string OrganizationId);
}
