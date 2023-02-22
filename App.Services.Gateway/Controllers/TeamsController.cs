using App.Services.Teams.Infrastructure.Grpc;
using App.Services.Teams.Infrastructure.Grpc.CommandMessages;
using Microsoft.AspNetCore.Mvc;

namespace App.Services.Gateway.Controllers
{
    [Route("api/[controller]")]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamsGrpcService _teamsGrpcService;

        public TeamsController(ITeamsGrpcService teamsGrpcService)
        {
            _teamsGrpcService = teamsGrpcService;
        }

        [HttpGet]
        [Route("Index")]
        public async ValueTask<IActionResult> Index()
        {
            var res = await this._teamsGrpcService.GetTeamsByOrganizationId(new GetTeamsByOrganizationIdCommandMessage() { Id = "lmao" });

            return Ok(res);
        }

        [HttpGet]
        [Route("{id}")]
        public async ValueTask<IActionResult> GetTeamById(string id)
        {
            var res = await this._teamsGrpcService.GetTeamById(new GetTeamByIdCommandMessage() { Id = id });

            return Ok(res);

            //return Ok(null);
        }
    }
}
