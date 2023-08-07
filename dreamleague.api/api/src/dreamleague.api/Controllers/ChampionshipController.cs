using dreamleague.domain.Aggregates.CreateChampionship;
using dreamleague.domain.Aggregates.DeleteChampionship;
using dreamleague.domain.Aggregates.GetChampionshipById;
using dreamleague.domain.Aggregates.GetChampionships;
using dreamleague.domain.Aggregates.GetChampionshipTeams;
using dreamleague.domain.Aggregates.RegisterTeamToChampionship;
using dreamleague.domain.Aggregates.RemoveTeamFromChampionship;
using dreamleague.domain.Aggregates.UpdateChampionship;
using dreamleague.domain.Services.Championship;
using Microsoft.AspNetCore.Mvc;

namespace dreamleague.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChampionshipsController : ControllerBase
    {
        private readonly ICreateChampionshipService createChampionshipService;
        private readonly IGetChampionshipsService getChampionshipsService;
        private readonly IGetChampionshipByIdService getChampionshipByIdService;
        private readonly IUpdateChampionshipService updateChampionshipService;
        private readonly IDeleteChampionshipService deleteChampionshipService;
        private readonly IRegisterTeamToChampionshipService registerTeamToChampionshipService;
        private readonly IGetChampionshipTeamsService getChampionshipTeamsService;
        private readonly IRemoveTeamFromChampionshipService removeTeamFromChampionshipRequest;
        public ChampionshipsController
            (
                ICreateChampionshipService createChampionshipService,
                IGetChampionshipsService getChampionshipsService,
                IGetChampionshipByIdService getChampionshipByIdService,
                IUpdateChampionshipService updateChampionshipService,
                IDeleteChampionshipService deleteChampionshipService,
                IRegisterTeamToChampionshipService registerTeamToChampionshipService,
                IGetChampionshipTeamsService getChampionshipTeamsService,
                IRemoveTeamFromChampionshipService removeTeamFromChampionshipRequest
            )
        {
            this.createChampionshipService = createChampionshipService;
            this.getChampionshipsService = getChampionshipsService;
            this.getChampionshipByIdService = getChampionshipByIdService;
            this.updateChampionshipService = updateChampionshipService;
            this.deleteChampionshipService = deleteChampionshipService;
            this.registerTeamToChampionshipService = registerTeamToChampionshipService;
            this.getChampionshipTeamsService = getChampionshipTeamsService;
            this.removeTeamFromChampionshipRequest = removeTeamFromChampionshipRequest;
        }

        /// GET /api/championships
        /// <summary>
        /// Gets all DreamLeague tournaments.
        /// </summary>
        [HttpGet]
        public async Task<OkObjectResult> GetChampionships()
        {
            var request = new GetChampionshipsRequest();
            return Ok(await getChampionshipsService.Execute(request));
        }

        /// GET /api/championships/{{championshipId}}/teams
        /// <summary>
        /// Gets all teams in a DreamLeague tournament.
        /// </summary>
        [HttpGet("{championshipId}/teams")]
        public async Task<OkObjectResult> GetChampionshipTeams([FromRoute] Guid? championshipId)
        {
            var request = new GetChampionshipTeamsRequest { ChampionshipId = championshipId.Value };
            return Ok(await getChampionshipTeamsService.Execute(request));
        }

        /// GET /api/championships/{{championshipId}}
        /// <summary>
        /// Gets a DreamLeague tournaments by its id.
        /// </summary>
        [HttpGet("{championshipId}")]
        public async Task<OkObjectResult> GetChampionshipById([FromRoute] Guid championshipId)
        {
            var request = new GetChampionshipByIdRequest { ChampionshipId = championshipId };
            return Ok(await getChampionshipByIdService.Execute(request));
        }

        /// POST /api/championships
        /// <summary>
        /// Create a DreamLeague tournament.
        /// </summary>
        [HttpPost]
        public async Task<OkObjectResult> CreateChampionship([FromBody] CreateChampionshipRequest request)
        {
            return Ok(await createChampionshipService.Execute(request));
        }

        /// POST /api/championships/{{championshipId}}/teams/{{teamId}}
        /// <summary>
        /// Create a DreamLeague tournament.
        /// </summary>
        [HttpPost("{championshipId}/teams/{teamId}")]
        public async Task<OkObjectResult> RegisterTeamToChampionship([FromRoute] Guid championshipId, [FromRoute] Guid teamId)
        {
            var request = new RegisterTeamToChampionshipRequest { ChampionshipId = championshipId, TeamId = teamId };
            return Ok(await registerTeamToChampionshipService.Execute(request));
        }


        /// PUT /api/championships
        /// <summary>
        /// Update a DreamLeague tournament.
        /// </summary>
        [HttpPut]
        public async Task<OkObjectResult> UpdateChampionship([FromBody] UpdateChampionshipRequest request)
        {
            return Ok(await updateChampionshipService.Execute(request));
        }

        /// DELETE /api/championships/{{championshipId}}
        /// <summary>
        /// Delete a DreamLeague tournament.
        /// </summary>
        [HttpDelete("{championshipId}")]
        public async Task<OkObjectResult> DeleteChampionship([FromRoute] Guid championshipId)
        {
            var request = new DeleteChampionshipRequest { ChampionshipId = championshipId };
            return Ok(await deleteChampionshipService.Execute(request));
        }

        /// DELETE /api/championships/{{championshipId}}/teams/{{teamId}}
        /// <summary>
        /// Delete a team from a DreamLeague tournament.
        /// </summary>
        [HttpDelete("{championshipId}/teams/{teamId}")]
        public async Task<OkObjectResult> DeleteTeamFromChampionship([FromRoute] Guid championshipId, [FromRoute] Guid teamId)
        {
            var request = new RemoveTeamFromChampionshipRequest { ChampionshipId = championshipId, TeamId = teamId };
            return Ok(await removeTeamFromChampionshipRequest.Execute(request));
        }
    }
}
