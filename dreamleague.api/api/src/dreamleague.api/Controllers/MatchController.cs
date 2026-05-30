using dreamleague.domain.Aggregates.GetMatchDetails;
using dreamleague.domain.Aggregates.MatchEvent;
using dreamleague.domain.Services.Match;
using Microsoft.AspNetCore.Mvc;

namespace dreamleague.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        private readonly IGetMatchDetailsService getMatchDetailsService;
        private readonly IMatchEventService matchEventService;

        public MatchController(
            IGetMatchDetailsService getMatchDetailsService,
            IMatchEventService matchEventService)
        {
            this.getMatchDetailsService = getMatchDetailsService;
            this.matchEventService = matchEventService;
        }

        /// <summary>GET /api/match/{matchId}/details — match details for the website.</summary>
        [HttpGet("{matchId}/details")]
        public async Task<OkObjectResult> GetMatchDetails([FromRoute] int matchId)
        {
            return Ok(await getMatchDetailsService.Execute(new GetMatchDetailsRequest { MatchId = matchId }));
        }

        /// <summary>POST /api/match/event — single webhook for all MatchZy events.</summary>
        [HttpPost("event")]
        public async Task<OkObjectResult> HandleEvent([FromBody] MatchEventRequest request)
        {
            return Ok(await matchEventService.Execute(request));
        }
    }
}
