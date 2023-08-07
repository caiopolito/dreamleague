using dreamleague.domain.Aggregates.CreateTeam;
using dreamleague.domain.Aggregates.DeleteTeam;
using dreamleague.domain.Aggregates.GetTeamById;
using dreamleague.domain.Aggregates.GetTeams;
using dreamleague.domain.Aggregates.InvitePlayersToTeam;
using dreamleague.domain.Aggregates.RemovePlayerFromTeam;
using dreamleague.domain.Aggregates.UpdateTeam;
using dreamleague.domain.Services.Team;
using Microsoft.AspNetCore.Mvc;

namespace dreamleague.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly ICreateTeamService createTeamService;
        private readonly IGetTeamByIdService getTeamByIdService;
        private readonly IGetTeamsService getTeamsService;
        private readonly IDeleteTeamService deleteTeamService;
        private readonly IUpdateTeamService updateTeamService;
        private readonly IInvitePlayersToTeamService invitePlayersToTeamService;
        private readonly IRemovePlayerFromTeamService removePlayerFromTeamService;
        public TeamsController
            (
                ICreateTeamService createTeamService,
                IGetTeamByIdService getTeamByIdService,
                IGetTeamsService getTeamsService,
                IDeleteTeamService deleteTeamService,
                IUpdateTeamService updateTeamService,
                IInvitePlayersToTeamService invitePlayersToTeamService,
                IRemovePlayerFromTeamService removePlayerFromTeamService
            )
        {
            this.createTeamService = createTeamService;
            this.getTeamByIdService = getTeamByIdService;
            this.deleteTeamService = deleteTeamService;
            this.updateTeamService = updateTeamService;
            this.getTeamsService = getTeamsService;
            this.invitePlayersToTeamService = invitePlayersToTeamService;
            this.removePlayerFromTeamService = removePlayerFromTeamService;
        }

        /// GET /api/teams/{{teamId}}
        /// <summary>
        /// Gets a DreamLeague team by its id.
        /// </summary>
        [HttpGet("{teamId}")]
        public async Task<OkObjectResult> GetTeamById([FromRoute] Guid teamId)
        {
            var request = new GetTeamByIdRequest { TeamId = teamId };
            return Ok(await getTeamByIdService.Execute(request));
        }

        /// GET /api/teams
        /// <summary>
        /// Gets all DreamLeague teams.
        /// </summary>
        [HttpGet()]
        public async Task<OkObjectResult> GetTeams([FromHeader] string steamId, [FromHeader] bool? isCaptain = null)
        {
            return Ok(await getTeamsService.Execute(new GetTeamsRequest { SteamId = steamId, IsCaptain = isCaptain }));
        }

        /// POST /api/teams
        /// <summary>
        /// Create a DreamLeague team.
        /// </summary>
        [HttpPost]
        public async Task<OkObjectResult> CreateTeam([FromBody] CreateTeamRequest request, [FromHeader] string? steamId = null)
        {
            request.SteamId = steamId;
            return Ok(await createTeamService.Execute(request));
        }


        /// PUT /api/teams
        /// <summary>
        /// Update a DreamLeague team.
        /// </summary>
        [HttpPut]
        public async Task<OkObjectResult> UpdateTeam([FromBody] UpdateTeamRequest request, [FromHeader] string? steamId = null)
        {
            request.SteamId = steamId;
            return Ok(await updateTeamService.Execute(request));
        }

        /// DELETE /api/teams/{{teamId}}
        /// <summary>
        /// Delete a DreamLeague team.
        /// </summary>
        [HttpDelete("{teamId}")]
        public async Task<OkObjectResult> DeleteTeam([FromRoute] Guid teamId)
        {
            var request = new DeleteTeamRequest { TeamId = teamId };
            return Ok(await deleteTeamService.Execute(request));
        }

        /// DELETE /api/teams/{{teamId}}/players/{{steamId}}
        /// <summary>
        /// Remove a player from a DreamLeague team.
        /// </summary>
        [HttpDelete("{teamId}/players/{steamId}")]
        public async Task<OkObjectResult> DeletePlayer([FromRoute] Guid teamId, [FromRoute] string steamId)
        {
            var request = new RemovePlayerFromTeamRequest { TeamId = teamId, SteamId = steamId };
            return Ok(await removePlayerFromTeamService.Execute(request));
        }

        /// POST /api/teams/{{teamId}}/invite
        /// <summary>
        /// Invite players to a DreamLeague team.
        /// </summary>
        [HttpPost("{teamId}/invite")]
        public async Task<OkObjectResult> InviteToTeam([FromBody] InvitePlayersToTeamRequest request, [FromRoute] Guid? teamId)
        {
            request.TeamId = teamId.Value;
            return Ok(await invitePlayersToTeamService.Execute(request));
        }
    }
}