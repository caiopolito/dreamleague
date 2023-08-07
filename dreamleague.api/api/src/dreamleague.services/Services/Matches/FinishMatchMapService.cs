using dreamleague.domain.Aggregates.FinishMatchMap;
using dreamleague.domain.Entities.Players;
using dreamleague.domain.Infrastructure;
using dreamleague.domain.Services.Match;
using dreamleague.shared.Services;

namespace dreamleague.services.Services.Matches
{
    public class FinishMatchMapService : GenericService<FinishMatchMapRequest, FinishMatchMapResponse>, IFinishMatchMapService
    {
        private readonly IUnitOfWork unitOfWork;
        public FinishMatchMapService
            (
                IUnitOfWork unitOfWork
            )
        {
            this.unitOfWork = unitOfWork;
        }
        protected async override Task<FinishMatchMapResponse> OnExecute(FinishMatchMapRequest request)
        {
            var match = await unitOfWork.MatchRepository.GetMatchByIdAsync(request.MatchId);

            var mapStats = await unitOfWork.MatchRepository.GetMatchMapStatsByMatchIdAsync(request.MatchId, request.MapNumber);

            if (request.Winner == "team1")
            {
                mapStats.Winner = match.Team1Id;
                match.Team1Score++;
            }
            else if (request.Winner == "team2")
            {
                mapStats.Winner = match.Team2Id;
                match.Team2Score++;
            }

            await unitOfWork.MatchRepository.UpdateMatchAsync(match);

            mapStats.EndTime = DateTime.UtcNow;

            await unitOfWork.MatchRepository.UpdateMatchMapStatsAsync(mapStats);

            var players = await unitOfWork.MatchRepository.GetPlayersByMatchIdAsync(request.MatchId);

            foreach (var player in players)
            {
                var playerStats = await unitOfWork.MatchRepository.GetMatchMapPlayerStatsAsync(request.MatchId, mapStats.Id, player);

                if (playerStats is not null)
                {
                    var points = playerStats.Kills - playerStats.Deaths + playerStats.HeadshotKill + 10;
                    await unitOfWork.PlayerRepository.UpdatePlayerAsync(new Player
                    {
                        SteamId = player,
                        Points = points,
                        Coins = new Random(3).Next(0, 50)
                    });
                }
            }


            return new FinishMatchMapResponse();
        }
    }
}
