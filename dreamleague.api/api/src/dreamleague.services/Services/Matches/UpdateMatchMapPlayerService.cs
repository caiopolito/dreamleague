using dreamleague.domain.Aggregates.UpdateMatchMapPlayer;
using dreamleague.domain.Entities.Get5;
using dreamleague.domain.Infrastructure;
using dreamleague.domain.Services.Match;
using dreamleague.shared.Services;

namespace dreamleague.services.Services.Matches
{
    public  class UpdateMatchMapPlayerService : GenericService<UpdateMatchMapPlayerRequest, UpdateMatchMapPlayerResponse>, IUpdateMatchMapPlayerService
    {
        private readonly IUnitOfWork unitOfWork;
        public UpdateMatchMapPlayerService
            (
                IUnitOfWork unitOfWork
            )
        {
            this.unitOfWork = unitOfWork;
        }
        protected async override Task<UpdateMatchMapPlayerResponse> OnExecute(UpdateMatchMapPlayerRequest request)
        {
            var match = await unitOfWork.MatchRepository.GetMatchByIdAsync(request.MatchId);

            var mapStats = await unitOfWork.MatchRepository.GetMatchMapStatsByMatchIdAsync(request.MatchId, request.MapNumber);

            var playerStats = await unitOfWork.MatchRepository.GetMatchMapPlayerStatsAsync(request.MatchId, mapStats.Id, request.SteamId);

            if (playerStats is null || playerStats == default)
                await unitOfWork.MatchRepository.CreateMatchMapPlayerStatsAsync(request.MatchId, mapStats.Id, request.SteamId);

            await unitOfWork.MatchRepository.UpdateMatchMapPlayerStatsAsync(new PlayerStats
            {
                MatchId = request.MatchId,
                MapId = mapStats.Id,
                SteamId = request.SteamId,
                Name = request.name,
                TeamId = request.team == "team1" ? match.Team1Id : match.Team2Id,
                Kills = request.kills,
                Assists = request.assists,
                Deaths = request.deaths,
                FlashbangAssists = request.flashbang_assists,
                Teamkills = request.teamkills,
                Suicides = request.suicides,
                Damage = request.damage,
                HeadshotKill = request.headshot_kills,
                RoundsPlayed = request.roundsplayed,
                BombPlants = request.bomb_plants,
                BombDefuses = request.bomb_defuses,
                k1 = request.k1,
                k2 = request.k2,
                k3 = request.k3,
                k4 = request.k4,
                k5 = request.k5,
                v1 = request.v1,
                v2 = request.v2,
                v3 = request.v3,
                v4 = request.v4,
                v5 = request.v5,
                FirstKillT = request.firstkill_t,
                FirstKillCt = request.firstkill_ct,
                FirstDeathT = request.firstdeath_t,
                FirstDeathCt = request.firstdeath_ct
            });

            return new UpdateMatchMapPlayerResponse();
        }
    }
}
