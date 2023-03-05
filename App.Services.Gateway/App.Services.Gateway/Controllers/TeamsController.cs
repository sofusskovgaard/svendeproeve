using App.Services.Gateway.Infrastructure;
using App.Services.Teams.Common.Dtos;
using App.Services.Teams.Infrastructure.Grpc;
using App.Services.Teams.Infrastructure.Grpc.CommandMessages;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using App.Services.Gateway.Common;

namespace App.Services.Gateway.Controllers
{
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class TeamsController : ApiController
    {
        private readonly ITeamsGrpcService _teamsGrpcService;

        public TeamsController(ITeamsGrpcService teamsGrpcService)
        {
            _teamsGrpcService = teamsGrpcService;
        }

        /// <summary>
        /// Get all teams
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Task<IActionResult> GetAllTeams()
        {
            return TryAsync(() => this._teamsGrpcService.GetAllTeams(new GetAllTeamsGrpcCommandMessage()));
        }

        /// <summary>
        /// Get all team in organization based on id of the organization id
        /// </summary>
        /// <param name="organizationid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{organizationid}/organization")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Task<IActionResult> GetTeamsByOrganizationId(string organizationid)
        {
            return TryAsync(() => this._teamsGrpcService.GetTeamsByOrganizationId(new GetTeamsByOrganizationIdGrpcCommandMessage() { OrganizationId = organizationid }));
        }

        /// <summary>
        /// Get all teams that has the same name as the given
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{name}/teamname")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Task<IActionResult> GetTeamsByName(string name)
        {
            return TryAsync(() => this._teamsGrpcService.GetTeamsByName(new GetTeamsByNameGrpcCommandMessage() { Name = name }));
        }

        /// <summary>
        /// Get all teams that has a member with given member id
        /// </summary>
        /// <param name="memberid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{memberid}/member")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Task<IActionResult> GetTeamsByMemberId(string memberid)
        {
            return TryAsync(() => this._teamsGrpcService.GetTeamsByMemberId(new GetTeamsByMemberIdGrpcCommandMessage() { MemberId = memberid }));
        }

        /// <summary>
        /// Get all teams that play a game based on the given gmae id
        /// </summary>
        /// <param name="gameid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{gameid}/game")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Task<IActionResult> GetTeamsByGameId(string gameid)
        {
            return TryAsync(() => this._teamsGrpcService.GetTeamsByGameId(new GetTeamsByGameIdGrpcCommandMessage() { GameId = gameid }));
        }

        /// <summary>
        /// Get all teams where the team manger has the same id as given
        /// </summary>
        /// <param name="managerid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{managerid}/manager")]
        public Task<IActionResult> GetTeamsByManagerId(string managerid)
        {
            return TryAsync(() => this._teamsGrpcService.GetTeamsByManagerId(new GetTeamsByManagerIdGrpcCommandMessage() { ManagerId = managerid }));
        }

        /// <summary>
        /// Get a team by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Task<IActionResult> GetTeamById(string id)
        {
            return TryAsync(() => this._teamsGrpcService.GetTeamById(new GetTeamByIdGrpcCommandMessage() { Id = id }));
        }

        /// <summary>
        /// Create a team
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("team")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<IActionResult> CreateTeam([FromBody] CreateTeamModel model)
        {
            return TryAsync(() =>
            {
                var command = new CreateTeamGrpcCommandMessage
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

        /// <summary>
        /// Delete a team
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<IActionResult> DeleteTeamById(string id)
        {
            return TryAsync(() => this._teamsGrpcService.DeleteTeamById(new DeleteTeamByIdGrpcCommandMessage() { Id = id }));
        }

        /// <summary>
        /// Update a team
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<IActionResult> UpdateTeam(string id, [FromBody] UpdateTeamModel model)
        {
            return TryAsync(() =>
            {
                var command = new UpdateTeamGrpcCommandMessage
                {
                    TeamId = id,
                    TeamDto = new UpdateTeamDto
                    {
                        Name = model.Name,
                        Bio = model.Bio,
                        ProfilePicturePath = model.ProfilePicturePath,
                        CoverPicturePath = model.CoverPicturePath
                    }
                };

                return _teamsGrpcService.UpdateTeam(command);
            });
        }
    }
}
