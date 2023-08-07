using dreamleague.domain.Aggregates.FinishMatch;
using dreamleague.domain.Aggregates.FinishMatchMap;
using dreamleague.domain.Aggregates.GetMatchDetails;
using dreamleague.domain.Aggregates.StartMatchMap;
using dreamleague.domain.Aggregates.UpdateMatchMap;
using dreamleague.domain.Aggregates.UpdateMatchMapPlayer;
using dreamleague.domain.Services.Match;
using Microsoft.AspNetCore.Mvc;

namespace dreamleague.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        private readonly IStartMatchMapService startMatchMapService;
        private readonly IUpdateMatchMapService updateMatchMapService;
        private readonly IUpdateMatchMapPlayerService updateMatchMapPlayerService;
        private readonly IFinishMatchMapService finishMatchMapService;
        private readonly IFinishMatchService finishMatchService;
        private readonly IGetMatchDetailsService getMatchDetailsService;
        public MatchController
            (
                IStartMatchMapService startMatchMapService,
                IUpdateMatchMapService updateMatchMapService,
                IUpdateMatchMapPlayerService updateMatchMapPlayerService,
                IFinishMatchMapService finishMatchMapService,
                IFinishMatchService finishMatchService,
                IGetMatchDetailsService getMatchDetailsService
            )
        {
            this.startMatchMapService = startMatchMapService;
            this.updateMatchMapService = updateMatchMapService;
            this.updateMatchMapPlayerService = updateMatchMapPlayerService;
            this.finishMatchMapService = finishMatchMapService;
            this.finishMatchService = finishMatchService;
            this.getMatchDetailsService = getMatchDetailsService;
        }


        /// GET /api/match/{{matchId}}/details
        /// <summary>
        /// Route designed to be called by the website to show a match detail.
        /// </summary>
        [HttpGet("{matchId}/details")]
        public async Task<OkObjectResult> FinishMatch([FromRoute] int matchId)
        {
            return Ok(await getMatchDetailsService.Execute(new GetMatchDetailsRequest { MatchId = matchId }));
        }

        /// POST /api/match/{{matchId}}/finish
        /// <summary>
        /// Route designed to be called by GET5 to finish a match that's happening.
        /// </summary>
        [HttpPost("{matchId}/finish")]
        public async Task<OkObjectResult> FinishMatch([FromRoute] int matchId, [FromForm] FinishMatchRequest request)
        {
            request.MatchId = matchId;
            return Ok(await finishMatchService.Execute(request));
        }

        /// POST /api/match/{{matchId}}/map/{{mapNumber}}/start
        /// <summary>
        /// Route designed to be called by GET5 to start a match map that's about to happen.
        [HttpPost("{matchId}/map/{mapNumber}/start")]
        public async Task<OkObjectResult> StartMatchMap([FromRoute] int matchId, [FromRoute] int mapNumber, [FromForm] StartMatchMapRequest request)
        {
            request.MatchId = matchId;
            request.MapNumber = mapNumber;
            return Ok(await startMatchMapService.Execute(request));
        }

        /// POST /api/match/{{matchId}}/map/{{mapNumber}}/update
        /// <summary>
        /// Route designed to be called by GET5 to update a match map that's happening.
        [HttpPost("{matchId}/map/{mapNumber}/update")]
        public async Task<OkObjectResult> UpdateMatchMap([FromRoute] int matchId, [FromRoute] int mapNumber, [FromForm] UpdateMatchMapRequest request)
        {
            request.MatchId = matchId;
            request.MapNumber = mapNumber;
            return Ok(await updateMatchMapService.Execute(request));
        }

        /// POST /api/match/{{matchId}}/map/{{mapNumber}}/finish
        /// <summary>
        /// Route designed to be called by GET5 to finish a match map that's ending.
        [HttpPost("{matchId}/map/{mapNumber}/finish")]
        public async Task<OkObjectResult> FinishMatchMap([FromRoute] int matchId, [FromRoute] int mapNumber, [FromForm] FinishMatchMapRequest request)
        {
            request.MatchId = matchId;
            request.MapNumber = mapNumber;
            return Ok(await finishMatchMapService.Execute(request));
        }

        /// POST /api/match/{{matchId}}/map/{{mapNumber}}/player/{{steamId}}/update
        /// <summary>
        /// Route designed to be called by GET5 to update a match map player's stats that's happening.
        [HttpPost("{matchId}/map/{mapNumber}/player/{steamId}/update")]
        public async Task<OkObjectResult> UpdateMatchMapPlayer([FromRoute] int matchId, [FromRoute] int mapNumber, [FromRoute] string steamId, [FromForm] UpdateMatchMapPlayerRequest request)
        {
            request.k1 = int.Parse(Request.Form.First(x => x.Key == "1kill_rounds").Value);
            request.k2 = int.Parse(Request.Form.First(x => x.Key == "2kill_rounds").Value);
            request.k3 = int.Parse(Request.Form.First(x => x.Key == "3kill_rounds").Value);
            request.k4 = int.Parse(Request.Form.First(x => x.Key == "4kill_rounds").Value);
            request.k5 = int.Parse(Request.Form.First(x => x.Key == "5kill_rounds").Value);
            request.MatchId = matchId;
            request.MapNumber = mapNumber;
            request.SteamId = steamId;
            return Ok(await updateMatchMapPlayerService.Execute(request));
        }
    }
}
