using App.Services.Gateway.Infrastructure;
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
        public async ValueTask<IActionResult> Index(string id)
        {
            var res = await this._teamsGrpcService.GetTeamsByOrganizationId(new GetTeamsByOrganizationIdCommandMessage() { OrganizationId = id });

            return Ok(res);
        }

        [HttpGet]
        [Route("name")]
        public async ValueTask<IActionResult> GetTeamsByName(string name)
        {
            var res = await this._teamsGrpcService.GetTeamsByName(new GetTeamsByNameCommandMessage() { Name = name });

            return Ok(res);
        }

        [HttpGet]
        [Route("memberteams")]
        public async ValueTask<IActionResult> GetTeamsByMemberId(string memberId)
        {
            var res = await this._teamsGrpcService.GetTeamsByMemberId(new GetTeamsByMemberIdCommandMessage() { MemberId = memberId });

            return Ok(res);
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
    }

    public record CreateTeamModel(string Name, string Bio, string ProfilePicturePath, string CoverPicturePath, string GameId, string[] MembersId, string ManagerId, string OrganizationId);
}
