using dreamleague.hubs.Hubs;
using dreamleague.domain.Aggregates.FinishMatch;
using dreamleague.domain.Entities.Matches;
using dreamleague.domain.Infrastructure;
using dreamleague.domain.Services.Match;
using dreamleague.shared.Services;
using Microsoft.AspNetCore.SignalR;

namespace dreamleague.services.Services.Matches
{
    public class FinishMatchService : GenericService<FinishMatchRequest, FinishMatchResponse>, IFinishMatchService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IHubContext<QueueHub> hubContext;
        private readonly IDictionary<string, OngoingMatch> playersInMatch;
        public FinishMatchService
            (
                IUnitOfWork unitOfWork,
                IHubContext<QueueHub> hubContext,
                IDictionary<string, OngoingMatch> playersInMatch
            )
        {
            this.unitOfWork = unitOfWork;
            this.playersInMatch = playersInMatch;
            this.hubContext = hubContext;
        }
        protected async override Task<FinishMatchResponse> OnExecute(FinishMatchRequest request)
        {
            Thread.Sleep(5000);

            var match = await unitOfWork.MatchRepository.GetMatchByIdAsync(request.MatchId);

            if (request.Forfeit == 1)
            {
                if (request.Winner == "none")
                {
                    match.Team1Score = 0;
                    match.Team2Score = 0;
                    match.Cancelled = true;
                }
                else
                {
                    match.Forfeit = true;
                    if (request.Winner == "team1") { match.Team1Score = 1; match.Team2Score = 0; }
                    else { match.Team1Score = 0; match.Team2Score = 1; }
                }
            }
            else
            {
                match.Winner = request.Winner == "team1" ? match.Team1Id : match.Team2Id;
            }
            match.EndTime = DateTime.UtcNow;

            await unitOfWork.MatchRepository.UpdateMatchAsync(match);

            var server = await unitOfWork.ServerRepository.GetServerByIdAsync(match.ServerId);

            await unitOfWork.ServerRepository.UpdateServerStatusAsync(match.ServerId, false);

            var keys = playersInMatch.Keys;

            foreach (var key in keys)
            {
                if (playersInMatch[key].Server.ConcatedIp == server.ConcatedIp)
                {
                    await hubContext.Clients.Group(match.MatchId).SendAsync("MatchEnd", match.MatchId);
                    playersInMatch.Remove(key);
                }
            }

            return new FinishMatchResponse { };
        }
    }
}
